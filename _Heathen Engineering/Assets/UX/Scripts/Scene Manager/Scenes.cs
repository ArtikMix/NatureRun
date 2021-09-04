using HeathenEngineering.Events;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace HeathenEngineering.UX
{
    /// <summary>
    /// Manages the multi-scene structure of your game.
    /// </summary>
    /// <remarks>
    /// This must be placed in your "0 bootstrap.scene"
    /// </remarks>
    public class Scenes : MonoBehaviour
    {
        public static Scenes manager;

        [Header("References")]
        #region Game Events
        public SceneProcessStateGameEvent Started; 
        public SceneProcessStateGameEvent Updated;
        public SceneProcessStateGameEvent Completed;
        #endregion

        [Header("Events")]
        #region Unity Events
        public UnitySceneProcessStateEvent evtStarted;
        public UnitySceneProcessStateEvent evtUpdated;
        public UnitySceneProcessStateEvent evtCompleted;
        #endregion

        /// <summary>
        /// Scene reference for the bootstrap scene (scene at build index 0)
        /// </summary>
        public static Scene Bootstrap
        {
            get
            {
                return SceneManager.GetSceneByBuildIndex(0);
            }
        }

        /// <summary>
        /// Returns true if a scene with this name is loaded
        /// </summary>
        /// <param name="sceneName">The name of the scene to test</param>
        /// <returns></returns>
        public static bool IsSceneLoaded(string sceneName)
        {
            return SceneManager.GetSceneByName(sceneName).isLoaded;
        }

        /// <summary>
        /// Returns true if the scene at this build index is loaded
        /// </summary>
        /// <param name="buildIndex">The build index of the scene to test</param>
        /// <returns></returns>
        public static bool IsSceneLoaded(int buildIndex)
        {
            return SceneManager.GetSceneByBuildIndex(buildIndex).isLoaded;
        }

        /// <summary>
        /// Creates a list reference of all the scenes that are currently loaded.
        /// </summary>
        public static List<Scene> Loaded
        {
            get
            {
                List<Scene> results = new List<Scene>();
                var count = SceneManager.sceneCount;
                for (int i = 0; i < count; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    if (scene.IsValid() && scene.isLoaded)
                        results.Add(scene);
                }

                return results;
            }
        }

        /// <summary>
        /// List the scenes available to be loaded
        /// </summary>
        public static List<Scene> Available
        {
            get
            {
                List<Scene> results = new List<Scene>();
                var count = SceneManager.sceneCountInBuildSettings;
                for (int i = 0; i < count; i++)
                {
                    results.Add(SceneManager.GetSceneByBuildIndex(i));
                }

                return results;
            }
        }

        void Start()
        {
            if (Scenes.manager != null)
            {
                Debug.LogWarning("Detected multiple ScenesManagers ... this is not supported please insure that 1 and only 1 ScenesManger exists and that it is loaded at the start and never reloaded or unloaded.");
                return;
            }

            Scenes.manager = this;
        }

        private static bool IsValid
        {
            get
            {
                if (manager == null)
                {
                    Debug.LogWarning("Scenes object is invalid, no manager has been found. Please add the Scenes componenet to a GameObejct and insure it persists through out the life of the game session.");
                    return false;
                }
                else
                    return true;
            }
        }

        public static void Transition(string from, string to, bool setActive, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var load = new List<string>();
            load.Add(to);

            var unload = new List<string>();
            unload.Add(from);

            manager.StartCoroutine(manager.ProcessState(unload, load, setActive ? to : null, callback));
        }

        public static void Transition(List<string> from, List<string> to, string setActive = null, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var load = new List<string>();
            foreach (var scene in to)
                load.Add(scene);

            var unload = new List<string>();
            foreach (var scene in from)
                unload.Add(scene);

            manager.StartCoroutine(manager.ProcessState(unload, load, setActive, callback));
        }

        /// <summary>
        /// Loads one scene while unloading another raising started, updated and completed events as required. 
        /// </summary>
        /// <param name="from">The scene to transition from, this will be unloaded</param>
        /// <param name="to">The scene to transition to, this will be loaded</param>
        /// <param name="setToAsActive">If true the "to" scene will be set as the active scene</param>
        /// <param name="action">Called when the process completes</param>
        public static void Transition(int from, int to, bool setToAsActive = false, UnityAction<SceneProcessState> action = null)
        {
            Transition(new int[] { from }, new int[] { to }, setToAsActive ? to : -1, action);
        }

        /// <summary>
        /// Loads one scene while unloading another raising started, updated and completed events as required. 
        /// </summary>
        /// <param name="from">The scenes to transition from, this will be unloaded</param>
        /// <param name="to">The scenes to transition to, this will be loaded</param>
        ///<param name="activeScene">The scene to set as thee active scene</param>
        /// <param name="action">Called when the process completes</param>
        public static void Transition(IEnumerable<int> from, IEnumerable<int> to, int activeScene = -1, UnityAction<SceneProcessState> action = null)
        {
            if (!IsValid)
                return;

            SceneProcessState nState = new SceneProcessState()
            {
                loadTargets = new List<int>(to),
                unloadTargets = new List<int>(from),
                setActiveScene = activeScene
            };

            manager.StartCoroutine(manager.ProcessState(nState, action));
        }

        public static void Load(string scene, bool setActive, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var load = new List<string>();
            load.Add(scene);

            manager.StartCoroutine(manager.ProcessState(null, load, setActive ? scene : null, callback));
        }

        public static void Load(List<string> scenes, string setActive = null, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var load = new List<string>();
            foreach (var scene in scenes)
                load.Add(scene);

            manager.StartCoroutine(manager.ProcessState(null, load, setActive, callback));
        }

        /// <summary>
        /// Loads a scene and optioanlly sets it as active
        /// </summary>
        /// <param name="scene">The build index of the scene to load</param>
        /// <param name="setActive">Should the scene be set active when loaded</param>
        /// <param name="callback">This is called when the process completes</param>
        public static void Load(int scene, bool setActive = false, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            if (scene >= 0 && scene < SceneManager.sceneCountInBuildSettings)
            {
                Load(new int[] { scene }, (setActive ? scene : -1), callback);
            }
            else
            {
                Debug.LogError("Attempted to load a scene by index, invalid scene index (" + scene + ") provided, no action taken!");
            }
        }

        /// <summary>
        /// Loads multiple scenes optionally setting one as active and calling an action when complete
        /// </summary>
        /// <param name="scenes">The scenes to load</param>
        /// <param name="activeScene">The scene to set active ... if -1 no scene will be set active</param>
        /// <param name="callback">The action to call when the process is complete</param>
        public static void Load(IEnumerable<int> scenes, int activeScene = -1, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            foreach(var scene in scenes)
            {
                if (scene < 0 || scene >= SceneManager.sceneCountInBuildSettings)
                {
                    Debug.LogError("Attempted to load a scene by index, invalid scene (" + scene + ") provided, no action taken!");
                    return;
                }
            }

            if (activeScene >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError("Attempted to load and activate a scene by index, invalid scene (" + activeScene + ") provided, no action taken!");
                return;
            }

            var nState = new SceneProcessState()
            {
                unloadTargets = new List<int>(),
                loadTargets = new List<int>(scenes),
                setActiveScene = activeScene,
            };

            manager.StartCoroutine(manager.ProcessState(nState, callback));
        }

        public static void Unload(string scene, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var unload = new List<string>();
            unload.Add(scene);

            manager.StartCoroutine(manager.ProcessState(unload, null, null, callback));
        }

        public static void Unload(List<string> scenes, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var unload = new List<string>();
            foreach (var scene in scenes)
                unload.Add(scene);

            manager.StartCoroutine(manager.ProcessState(unload, null, null, callback));
        }

        /// <summary>
        /// Unloads the indicated scene and calls the indicated action when complete
        /// </summary>
        /// <param name="scene">The scene to unload</param>
        /// <param name="callback">This is called when the process is complete</param>
        public static void Unload(int scene, UnityAction<SceneProcessState> callback = null)
        {
            Unload(new int[] { scene }, callback);
        }

        /// <summary>
        /// Unloads multiple scenes if they are loaded and calls the indicated action when the process is complete
        /// </summary>
        /// <param name="scenes">The scenes to be unloaded</param>
        /// <param name="callback">The action to call when complete</param>
        public static void Unload(IEnumerable<int> scenes, UnityAction<SceneProcessState> callback = null)
        {
            if (!IsValid)
                return;

            var nState = new SceneProcessState()
            {
                unloadTargets = new List<int>(scenes),
                loadTargets = new List<int>(),
                setActiveScene = -1,
            };

            manager.StartCoroutine(manager.ProcessState(nState, callback));
        }

        /// <summary>
        /// Sets the indicated scene as active if its loaded.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene to set active</param>
        public static void SetSceneActive(int buildIndex)
        {
            if (buildIndex < 0)
            {
                Debug.LogWarning("Attempted to set an invalid scene index as active. No Action Was Taken!");
                return;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));
        }

        private IEnumerator ProcessState(SceneProcessState state, UnityAction<SceneProcessState> callback)
        {
            state.loadProgress = 0;
            state.unloadProgress = 0;
            state.transitionProgress = 0;
            state.complete = false;
            state.hasError = false;

            if (Started != null)
                Started.Raise(this, state);

            evtStarted.Invoke(state);
            
            callback?.Invoke(state);

            //Validate the entries
            if(state.loadTargets != null)
            {
                foreach(var scene in state.loadTargets)
                {
                    if(scene < 0 || SceneManager.sceneCountInBuildSettings <= scene)
                    {
                        state.hasError = true;
                        state.errorMessage += "Load target index (" + scene + ") is out of range.\n";
                    }
                }
            }

            if (state.unloadTargets != null)
            {
                foreach (var scene in state.unloadTargets)
                {
                    if (scene < 0 || SceneManager.sceneCountInBuildSettings <= scene)
                    {
                        state.hasError = true;
                        state.errorMessage += "Unload target index (" + scene + ") is out of range.\n";
                    }
                }
            }

            if(state.setActiveScene >= SceneManager.sceneCountInBuildSettings)
            {
                state.hasError = true;
                state.errorMessage += "The set active scene target (" + state.setActiveScene + ") is invalid.\n";
            }

            if (!state.hasError)
            {
                yield return new WaitForEndOfFrame();

                List<AsyncOperation> loadOperations = new List<AsyncOperation>();
                List<AsyncOperation> unloadOperations = new List<AsyncOperation>();

                if (state.loadTargets != null)
                {
                    foreach (var scene in state.loadTargets)
                    {
                        if (!Scenes.IsSceneLoaded(scene))
                        {
                            var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                            op.allowSceneActivation = true;
                            loadOperations.Add(op);
                        }
                    }
                }

                if (state.unloadTargets != null)
                {
                    foreach (var scene in state.unloadTargets)
                    {
                        if (Scenes.IsSceneLoaded(scene))
                        {
                            var op = SceneManager.UnloadSceneAsync(scene);
                            op.allowSceneActivation = true;
                            unloadOperations.Add(op);
                        }
                    }
                }

                int loadCount = loadOperations.Count;
                int unloadCount = unloadOperations.Count;

                while (loadOperations.Count > 0 || unloadOperations.Count > 0)
                {
                    loadOperations.RemoveAll(p => p.isDone);
                    unloadOperations.RemoveAll(p => p.isDone);

                    float loadProgress = loadCount - loadOperations.Count;
                    float unloadProgress = unloadCount - unloadOperations.Count;

                    foreach (var op in loadOperations)
                    {
                        loadProgress += op.progress;
                    }

                    foreach (var op in unloadOperations)
                    {
                        unloadProgress += op.progress;
                    }

                    if (loadCount > 0)
                        loadProgress = loadProgress / loadCount;
                    else
                        loadProgress = 1f;

                    if (unloadCount > 0)
                        unloadProgress = unloadProgress / unloadCount;
                    else
                        unloadProgress = 1f;

                    state.loadProgress = loadProgress;
                    state.unloadProgress = unloadProgress;
                    if (loadCount > 0 && unloadCount > 0)
                        state.transitionProgress = (loadProgress + unloadProgress) / 2f;
                    else if (loadCount > 0)
                        state.transitionProgress = loadProgress;
                    else
                        state.transitionProgress = unloadProgress;

                    if (Updated != null)
                        Updated.Raise(this, state);

                    evtUpdated.Invoke(state);

                    callback?.Invoke(state);

                    yield return new WaitForEndOfFrame();
                }

                if (state.setActiveScene >= 0)
                    SetSceneActive(state.setActiveScene);
            }

            state.complete = true;

            if (Completed != null)
                Completed.Raise(this, state);

            evtCompleted.Invoke(state);

            callback?.Invoke(state);
        }

        private IEnumerator ProcessState(List<string> from, List<string> to, string toActive, UnityAction<SceneProcessState> callback)
        {
            SceneProcessState state = new SceneProcessState();

            state.loadProgress = 0;
            state.unloadProgress = 0;
            state.transitionProgress = 0;
            state.complete = false;
            state.hasError = false;

            if (Started != null)
                Started.Raise(this, state);

            evtStarted.Invoke(state);

            callback?.Invoke(state);

            yield return new WaitForEndOfFrame();

            List<AsyncOperation> loadOperations = new List<AsyncOperation>();
            List<AsyncOperation> unloadOperations = new List<AsyncOperation>();

            if (to != null)
            {
                foreach (var scene in to)
                {
                    if (!Scenes.IsSceneLoaded(scene))
                    {
                        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                        op.allowSceneActivation = true;
                        loadOperations.Add(op);
                    }
                }
            }

            if (from != null)
            {
                foreach (var scene in from)
                {
                    if (Scenes.IsSceneLoaded(scene))
                    {
                        var op = SceneManager.UnloadSceneAsync(scene);
                        op.allowSceneActivation = true;
                        unloadOperations.Add(op);
                    }
                }
            }

            int loadCount = loadOperations.Count;
            int unloadCount = unloadOperations.Count;

            while (loadOperations.Count > 0 || unloadOperations.Count > 0)
            {
                loadOperations.RemoveAll(p => p.isDone);
                unloadOperations.RemoveAll(p => p.isDone);

                float loadProgress = loadCount - loadOperations.Count;
                float unloadProgress = unloadCount - unloadOperations.Count;

                foreach (var op in loadOperations)
                {
                    loadProgress += op.progress;
                }

                foreach (var op in unloadOperations)
                {
                    unloadProgress += op.progress;
                }

                if (loadCount > 0)
                    loadProgress = loadProgress / loadCount;
                else
                    loadProgress = 1f;

                if (unloadCount > 0)
                    unloadProgress = unloadProgress / unloadCount;
                else
                    unloadProgress = 1f;

                state.loadProgress = loadProgress;
                state.unloadProgress = unloadProgress;
                if (loadCount > 0 && unloadCount > 0)
                    state.transitionProgress = (loadProgress + unloadProgress) / 2f;
                else if (loadCount > 0)
                    state.transitionProgress = loadProgress;
                else
                    state.transitionProgress = unloadProgress;

                if (Updated != null)
                    Updated.Raise(this, state);

                evtUpdated.Invoke(state);

                callback?.Invoke(state);

                yield return new WaitForEndOfFrame();
            }

            if (!string.IsNullOrEmpty(toActive))
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(toActive));

            state.complete = true;

            if (Completed != null)
                Completed.Raise(this, state);

            evtCompleted.Invoke(state);

            callback?.Invoke(state);
        }
    }
}
