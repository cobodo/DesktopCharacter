#pragma once

#include "CppSharp.h"
#include <Live2DWrapping/Live2DWrap.h>

namespace Live2DWrap
{
    ref class Live2DAnimation;
    ref class Live2DMotionQueueManager;
    ref class Live2DWrapping;
    ref class Live2Model;
    namespace live2d
    {
        ref class Live2DModelWinGL;
        ref class Live2DMotion;
        ref class MotionQueueManager;
    }
}

namespace Live2DWrap
{
    public ref class Live2DWrapping : ICppInstance
    {
    public:

        property ::Live2DWrapping* NativePtr;
        property System::IntPtr __Instance
        {
            virtual System::IntPtr get();
            virtual void set(System::IntPtr instance);
        }

        Live2DWrapping(::Live2DWrapping* native);
        static Live2DWrapping^ __CreateInstance(::System::IntPtr native);
        Live2DWrapping();

        Live2DWrapping(Live2DWrap::Live2DWrapping^ _0);

        ~Live2DWrapping();

        static void init();

        static void destory();

        protected:
        bool __ownsNativeInstance;
    };

    public ref class Live2Model : ICppInstance
    {
    public:

        property ::Live2Model* NativePtr;
        property System::IntPtr __Instance
        {
            virtual System::IntPtr get();
            virtual void set(System::IntPtr instance);
        }

        Live2Model(::Live2Model* native);
        static Live2Model^ __CreateInstance(::System::IntPtr native);
        Live2Model();

        Live2Model(Live2DWrap::Live2Model^ _0);

        ~Live2Model();

        void craeteModel(System::String^ str);

        void setTexture(int textureNo, unsigned int glTexture);

        void setPremultipliedAlpha(bool r);

        float getCanvasWidth();

        float getCanvasHeight();

        void setMatrix(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33);

        void setPrameterFloat(int index, float value, float wieght);

        void setPrameterFloat(System::String^ name, float value, float wieght);

        /// <summary>
        /// :
        /// </summary>
        void update();

        /// <summary>
        /// :
        /// </summary>
        void draw();

        protected:
        bool __ownsNativeInstance;
    };

    public ref class Live2DAnimation : ICppInstance
    {
    public:

        property ::Live2DAnimation* NativePtr;
        property System::IntPtr __Instance
        {
            virtual System::IntPtr get();
            virtual void set(System::IntPtr instance);
        }

        Live2DAnimation(::Live2DAnimation* native);
        static Live2DAnimation^ __CreateInstance(::System::IntPtr native);
        Live2DAnimation();

        Live2DAnimation(Live2DWrap::Live2DAnimation^ _0);

        ~Live2DAnimation();

        /// <summary>
        /// :
        /// </summary>
        void loadMotion(System::String^ filepath);

        /// <summary>
        /// :
        /// </summary>
        void updateParamExe(Live2DWrap::Live2Model^ model, long long timeMSec, float weight);

        /// <summary>
        /// :
        /// </summary>
        void setLoop(bool loop);

        /// <summary>
        /// :
        /// </summary>
        bool isLoop();

        /// <summary>
        /// :
        /// </summary>
        void setLoopFadeIn(bool loopFadeIn);

        /// <summary>
        /// :
        /// </summary>
        bool isLoopFadeIn();

        protected:
        bool __ownsNativeInstance;
    };

    public ref class Live2DMotionQueueManager : ICppInstance
    {
    public:

        property ::Live2DMotionQueueManager* NativePtr;
        property System::IntPtr __Instance
        {
            virtual System::IntPtr get();
            virtual void set(System::IntPtr instance);
        }

        Live2DMotionQueueManager(::Live2DMotionQueueManager* native);
        static Live2DMotionQueueManager^ __CreateInstance(::System::IntPtr native);
        Live2DMotionQueueManager();

        Live2DMotionQueueManager(Live2DWrap::Live2DMotionQueueManager^ _0);

        ~Live2DMotionQueueManager();

        /// <summary>
        /// :
        /// </summary>
        int startMotion(Live2DWrap::Live2DAnimation^ motion, bool autoDelete);

        /// <summary>
        /// :
        /// </summary>
        bool updateParam(Live2DWrap::Live2Model^ model);

        /// <summary>
        /// :
        /// </summary>
        bool isFinished();

        /// <summary>
        /// :
        /// </summary>
        bool isFinished(int motionQueueEntNo);

        /// <summary>
        /// :
        /// </summary>
        void stopAllMotions();

        protected:
        bool __ownsNativeInstance;
    };

    namespace live2d
    {
    }
}
