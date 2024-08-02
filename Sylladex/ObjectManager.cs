using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;

namespace Sylladex
{
    public abstract class ObjectManager<TKey, TValue>
    {
        protected Dictionary<TKey, TValue> _objects = new Dictionary<TKey, TValue>();

        public void AddObject(TKey key, TValue asset)
        {
            _objects[key] = asset;
        }

        public TValue GetObject(TKey key)
        {
            if (_objects.ContainsKey(key))
            {
                return _objects[key];
            }
            else
            {
                throw new KeyNotFoundException($"'{typeof(TValue).GetType()}': Object with key '{key}' not found.");
            }
        }
    }
    public abstract class ObjectManager<T> : ObjectManager<string, T> { }
    public class TextureManager : ObjectManager<Texture2D> { }
    public class AnimationManager : ObjectManager<Animation> { }

    public class SoundEffectManager : ObjectManager<SoundEffect>
    {
        private readonly Dictionary<string, SoundEffectInstance> _instances = new Dictionary<string, SoundEffectInstance>();
        public void Play(string key)
        {
            if (!_instances.ContainsKey(key))
            {
                _instances[key] = GetObject(key).CreateInstance();
            }
            if (_instances[key].State == SoundState.Stopped)
            {
                _instances[key].Play();
            }
        }
    }

    public class SoundtrackManager : ObjectManager<Song>
    {
        public void Play(string key, bool shouldLoop = false)
        {
            MediaPlayer.Play(GetObject(key));
            MediaPlayer.IsRepeating = shouldLoop;
        }
    }
    public class EntityManager : ObjectManager<Entity>
    {
        public void Update()
        {
            foreach (var entity in _objects.Values)
            {
                entity.Update();
            }
        }
        public void Draw()
        {
            foreach (var entity in _objects.Values)
            {
                entity.Draw();
            }
        }
    }

    public class InputManager : ObjectManager<Keys, Action>
    {
        public void Update()
        {
            KeyboardState kstate = Keyboard.GetState();
            foreach (var key in _objects)
            {
                if (kstate.IsKeyDown(key.Key))
                {
                    key.Value();
                }
            }
        }
    }

    public class WindowManager : ObjectManager<string, Window>
    {
        public void Draw()
        {
            foreach (var window in _objects.Values)
            {
                if (window.IsVisible())
                {
                    window.Draw();
                }
            }
        }
    }
}
