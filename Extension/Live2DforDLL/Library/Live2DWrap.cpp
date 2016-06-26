#include "Live2DWrap.h"

using namespace System;
using namespace System::Runtime::InteropServices;

Live2DWrap::Live2DWrapping::Live2DWrapping(::Live2DWrapping* native)
    : __ownsNativeInstance(false)
{
    NativePtr = native;
}

Live2DWrap::Live2DWrapping^ Live2DWrap::Live2DWrapping::__CreateInstance(::System::IntPtr native)
{
    return gcnew ::Live2DWrap::Live2DWrapping((::Live2DWrapping*) native.ToPointer());
}

Live2DWrap::Live2DWrapping::~Live2DWrapping()
{
    delete NativePtr;
}

void Live2DWrap::Live2DWrapping::init()
{
    ::Live2DWrapping::init();
}

void Live2DWrap::Live2DWrapping::destory()
{
    ::Live2DWrapping::destory();
}

Live2DWrap::Live2DWrapping::Live2DWrapping()
{
    __ownsNativeInstance = true;
    NativePtr = new ::Live2DWrapping();
}

Live2DWrap::Live2DWrapping::Live2DWrapping(Live2DWrap::Live2DWrapping^ _0)
{
    __ownsNativeInstance = true;
    if (ReferenceEquals(_0, nullptr))
        throw gcnew ::System::ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
    auto &arg0 = *(::Live2DWrapping*)_0->NativePtr;
    NativePtr = new ::Live2DWrapping(arg0);
}

System::IntPtr Live2DWrap::Live2DWrapping::__Instance::get()
{
    return System::IntPtr(NativePtr);
}

void Live2DWrap::Live2DWrapping::__Instance::set(System::IntPtr object)
{
    NativePtr = (::Live2DWrapping*)object.ToPointer();
}
Live2DWrap::Live2Model::Live2Model(::Live2Model* native)
    : __ownsNativeInstance(false)
{
    NativePtr = native;
}

Live2DWrap::Live2Model^ Live2DWrap::Live2Model::__CreateInstance(::System::IntPtr native)
{
    return gcnew ::Live2DWrap::Live2Model((::Live2Model*) native.ToPointer());
}

Live2DWrap::Live2Model::~Live2Model()
{
    delete NativePtr;
}

Live2DWrap::Live2Model::Live2Model()
{
    __ownsNativeInstance = true;
    NativePtr = new ::Live2Model();
}

void Live2DWrap::Live2Model::craeteModel(System::String^ str)
{
    auto _arg0 = clix::marshalString<clix::E_UTF8>(str);
    auto arg0 = _arg0.c_str();
    ((::Live2Model*)NativePtr)->craeteModel(arg0);
}

void Live2DWrap::Live2Model::deleteModel()
{
    ((::Live2Model*)NativePtr)->deleteModel();
}

void Live2DWrap::Live2Model::setTexture(int textureNo, unsigned int glTexture)
{
    ((::Live2Model*)NativePtr)->setTexture(textureNo, glTexture);
}

void Live2DWrap::Live2Model::setPremultipliedAlpha(bool r)
{
    ((::Live2Model*)NativePtr)->setPremultipliedAlpha(r);
}

float Live2DWrap::Live2Model::getCanvasWidth()
{
    auto __ret = ((::Live2Model*)NativePtr)->getCanvasWidth();
    return __ret;
}

float Live2DWrap::Live2Model::getCanvasHeight()
{
    auto __ret = ((::Live2Model*)NativePtr)->getCanvasHeight();
    return __ret;
}

void Live2DWrap::Live2Model::setMatrix(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
{
    ((::Live2Model*)NativePtr)->setMatrix(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
}

void Live2DWrap::Live2Model::setPrameterFloat(int index, float value, float wieght)
{
    ((::Live2Model*)NativePtr)->setPrameterFloat(index, value, wieght);
}

void Live2DWrap::Live2Model::setPrameterFloat(System::String^ name, float value, float wieght)
{
    auto _arg0 = clix::marshalString<clix::E_UTF8>(name);
    auto arg0 = _arg0.c_str();
    ((::Live2Model*)NativePtr)->setPrameterFloat(arg0, value, wieght);
}

void Live2DWrap::Live2Model::update()
{
    ((::Live2Model*)NativePtr)->update();
}

void Live2DWrap::Live2Model::draw()
{
    ((::Live2Model*)NativePtr)->draw();
}

Live2DWrap::Live2Model::Live2Model(Live2DWrap::Live2Model^ _0)
{
    __ownsNativeInstance = true;
    if (ReferenceEquals(_0, nullptr))
        throw gcnew ::System::ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
    auto &arg0 = *(::Live2Model*)_0->NativePtr;
    NativePtr = new ::Live2Model(arg0);
}

System::IntPtr Live2DWrap::Live2Model::__Instance::get()
{
    return System::IntPtr(NativePtr);
}

void Live2DWrap::Live2Model::__Instance::set(System::IntPtr object)
{
    NativePtr = (::Live2Model*)object.ToPointer();
}
Live2DWrap::Live2DAnimation::Live2DAnimation(::Live2DAnimation* native)
    : __ownsNativeInstance(false)
{
    NativePtr = native;
}

Live2DWrap::Live2DAnimation^ Live2DWrap::Live2DAnimation::__CreateInstance(::System::IntPtr native)
{
    return gcnew ::Live2DWrap::Live2DAnimation((::Live2DAnimation*) native.ToPointer());
}

Live2DWrap::Live2DAnimation::~Live2DAnimation()
{
    delete NativePtr;
}

Live2DWrap::Live2DAnimation::Live2DAnimation()
{
    __ownsNativeInstance = true;
    NativePtr = new ::Live2DAnimation();
}

void Live2DWrap::Live2DAnimation::loadMotion(System::String^ filepath)
{
    auto _arg0 = clix::marshalString<clix::E_UTF8>(filepath);
    auto arg0 = _arg0.c_str();
    ((::Live2DAnimation*)NativePtr)->loadMotion(arg0);
}

void Live2DWrap::Live2DAnimation::deleteMotion()
{
    ((::Live2DAnimation*)NativePtr)->deleteMotion();
}

void Live2DWrap::Live2DAnimation::updateParamExe(Live2DWrap::Live2Model^ model, long long timeMSec, float weight)
{
    auto arg0 = (::Live2Model*)model->NativePtr;
    ((::Live2DAnimation*)NativePtr)->updateParamExe(arg0, timeMSec, weight);
}

void Live2DWrap::Live2DAnimation::setLoop(bool loop)
{
    ((::Live2DAnimation*)NativePtr)->setLoop(loop);
}

bool Live2DWrap::Live2DAnimation::isLoop()
{
    auto __ret = ((::Live2DAnimation*)NativePtr)->isLoop();
    return __ret;
}

void Live2DWrap::Live2DAnimation::setLoopFadeIn(bool loopFadeIn)
{
    ((::Live2DAnimation*)NativePtr)->setLoopFadeIn(loopFadeIn);
}

bool Live2DWrap::Live2DAnimation::isLoopFadeIn()
{
    auto __ret = ((::Live2DAnimation*)NativePtr)->isLoopFadeIn();
    return __ret;
}

Live2DWrap::Live2DAnimation::Live2DAnimation(Live2DWrap::Live2DAnimation^ _0)
{
    __ownsNativeInstance = true;
    if (ReferenceEquals(_0, nullptr))
        throw gcnew ::System::ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
    auto &arg0 = *(::Live2DAnimation*)_0->NativePtr;
    NativePtr = new ::Live2DAnimation(arg0);
}

System::IntPtr Live2DWrap::Live2DAnimation::__Instance::get()
{
    return System::IntPtr(NativePtr);
}

void Live2DWrap::Live2DAnimation::__Instance::set(System::IntPtr object)
{
    NativePtr = (::Live2DAnimation*)object.ToPointer();
}
Live2DWrap::Live2DMotionQueueManager::Live2DMotionQueueManager(::Live2DMotionQueueManager* native)
    : __ownsNativeInstance(false)
{
    NativePtr = native;
}

Live2DWrap::Live2DMotionQueueManager^ Live2DWrap::Live2DMotionQueueManager::__CreateInstance(::System::IntPtr native)
{
    return gcnew ::Live2DWrap::Live2DMotionQueueManager((::Live2DMotionQueueManager*) native.ToPointer());
}

Live2DWrap::Live2DMotionQueueManager::~Live2DMotionQueueManager()
{
    delete NativePtr;
}

Live2DWrap::Live2DMotionQueueManager::Live2DMotionQueueManager()
{
    __ownsNativeInstance = true;
    NativePtr = new ::Live2DMotionQueueManager();
}

void Live2DWrap::Live2DMotionQueueManager::deleteMotionManager()
{
    ((::Live2DMotionQueueManager*)NativePtr)->deleteMotionManager();
}

int Live2DWrap::Live2DMotionQueueManager::startMotion(Live2DWrap::Live2DAnimation^ motion, bool autoDelete)
{
    auto arg0 = (::Live2DAnimation*)motion->NativePtr;
    auto __ret = ((::Live2DMotionQueueManager*)NativePtr)->startMotion(arg0, autoDelete);
    return __ret;
}

bool Live2DWrap::Live2DMotionQueueManager::updateParam(Live2DWrap::Live2Model^ model)
{
    auto arg0 = (::Live2Model*)model->NativePtr;
    auto __ret = ((::Live2DMotionQueueManager*)NativePtr)->updateParam(arg0);
    return __ret;
}

bool Live2DWrap::Live2DMotionQueueManager::isFinished()
{
    auto __ret = ((::Live2DMotionQueueManager*)NativePtr)->isFinished();
    return __ret;
}

bool Live2DWrap::Live2DMotionQueueManager::isFinished(int motionQueueEntNo)
{
    auto __ret = ((::Live2DMotionQueueManager*)NativePtr)->isFinished(motionQueueEntNo);
    return __ret;
}

void Live2DWrap::Live2DMotionQueueManager::stopAllMotions()
{
    ((::Live2DMotionQueueManager*)NativePtr)->stopAllMotions();
}

Live2DWrap::Live2DMotionQueueManager::Live2DMotionQueueManager(Live2DWrap::Live2DMotionQueueManager^ _0)
{
    __ownsNativeInstance = true;
    if (ReferenceEquals(_0, nullptr))
        throw gcnew ::System::ArgumentNullException("_0", "Cannot be null because it is a C++ reference (&).");
    auto &arg0 = *(::Live2DMotionQueueManager*)_0->NativePtr;
    NativePtr = new ::Live2DMotionQueueManager(arg0);
}

System::IntPtr Live2DWrap::Live2DMotionQueueManager::__Instance::get()
{
    return System::IntPtr(NativePtr);
}

void Live2DWrap::Live2DMotionQueueManager::__Instance::set(System::IntPtr object)
{
    NativePtr = (::Live2DMotionQueueManager*)object.ToPointer();
}
