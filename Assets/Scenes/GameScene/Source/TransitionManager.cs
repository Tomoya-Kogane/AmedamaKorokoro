using System;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AmedamaKorokoro.Scripts.Utilities.DataPacks;

/// <summary>
/// シーン遷移管理
/// </summary>
namespace AmedamaKorokoro.Scripts.Utilities.Transition
{
    /// <summary>
    /// シーン遷移を管理するクラス
    /// </summary>
    public class TransitionManager
    {
        //
        private bool _isRunning = false;
        public bool IsRunning
        { 
            get { return _isRunning; } 
        }

        //
        private ScenesList _currentScene;
        public ScenesList CurrentScene
        {
            get { return _currentScene; }
        }

        //
        private Subject<Unit> _onTransactionFinishedIntarnal = new Subject<Unit>();

        //
        private Subject<Unit> _onTransitionAnimationFinishedSubject = new Subject<Unit>();
        public IObservable<Unit> OnTransitionAnimationFinished
        {
            get
            {
                if (_isRunning)
                {
                    return _onTransitionAnimationFinishedSubject.FirstOrDefault();
                }
                else
                {
                    return Observable.Return(Unit.Default);
                }
            }
        }


        //
        private Subject<Unit> _onAllSceneLoaded = new Subject<Unit>();
        public IObservable<Unit> OnScenesLoaded
        {  
            get { return _onAllSceneLoaded; } 
        }
    }
}