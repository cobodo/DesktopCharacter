#include "Live2DWrap.h"
#include "include/Live2D.h"
#include "include/motion/Live2DMotion.h"
#include "include/Live2DModelWinGL.h"
#include "include/util/UtSystem.h"
#include "include/motion/MotionQueueManager.h"

using namespace live2d;

void Live2DWrapping::init()
{
	live2d::Live2D::init();
}

void Live2DWrapping::destory()
{
	live2d::Live2D::dispose();
}

Live2Model::Live2Model()
{
}

Live2Model::~Live2Model()
{
}

void Live2Model::craeteModel( const char* str )
{
	mLive2DModel = Live2DModelWinGL::loadModel( str );
}

void Live2Model::setTexture( int textureNo, unsigned int glTexture )
{
	mLive2DModel->setTexture( textureNo, glTexture );
}

void Live2Model::setPremultipliedAlpha( bool r )
{
	mLive2DModel->setPremultipliedAlpha( r );
}

float Live2Model::getCanvasWidth() const
{
	return mLive2DModel->getCanvasWidth();
}

float Live2Model::getCanvasHeight() const
{
	return mLive2DModel->getCanvasHeight();
}

void Live2Model::setMatrix(
	float m00, float m01, float m02, float m03,
	float m10, float m11, float m12, float m13,
	float m20, float m21, float m22, float m23,
	float m30, float m31, float m32, float m33 )
{
	float matrix[ 16 ] = {
		m00, m01, m02, m03,
		m10, m11, m12, m13,
		m20, m21, m22, m23,
		m30, m31, m32, m33
	};
	mLive2DModel->setMatrix( matrix );
}

void Live2Model::setPrameterFloat( int index, float value, float wieght /*= 1.f */ )
{
	mLive2DModel->setParamFloat( index, value, wieght );
}

void Live2Model::setPrameterFloat( const char* name, float value, float wieght /*= 1.f */ )
{
	mLive2DModel->setParamFloat( name, value, wieght );
}

void Live2Model::update()
{
	mLive2DModel->update();
}

void Live2Model::draw()
{
	mLive2DModel->draw();
}

live2d::Live2DModelWinGL* Live2Model::getModel() const
{
	return mLive2DModel;
}

Live2DAnimation::Live2DAnimation() : mMotion()
{

}

Live2DAnimation::~Live2DAnimation()
{

}

void Live2DAnimation::loadMotion( const char* filepath )
{
	mMotion = live2d::Live2DMotion::loadMotion( filepath );
}

void Live2DAnimation::updateParamExe( Live2Model*  model, long long timeMSec, float weight )
{
	mMotion->updateParamExe( model->getModel(), timeMSec, weight, nullptr );
}

void Live2DAnimation::setLoop( bool loop )
{
	mMotion->setLoop( loop );
}

bool Live2DAnimation::isLoop()
{
	return mMotion->isLoop();
}

void Live2DAnimation::setLoopFadeIn( bool loopFadeIn )
{
	mMotion->setLoopFadeIn( loopFadeIn );
}

bool Live2DAnimation::isLoopFadeIn()
{
	return mMotion->isLoopFadeIn();
}

live2d::Live2DMotion* Live2DAnimation::getMotion() const
{
	return mMotion;
}

Live2DMotionQueueManager::Live2DMotionQueueManager() : mMotionManager()
{
	mMotionManager = new live2d::MotionQueueManager();
}

Live2DMotionQueueManager::~Live2DMotionQueueManager() 
{
	delete mMotionManager;
	mMotionManager = nullptr;
}

int Live2DMotionQueueManager::startMotion( Live2DAnimation* motion, bool autoDelete )
{
	return mMotionManager->startMotion( motion->getMotion(), autoDelete );
}

bool Live2DMotionQueueManager::updateParam( Live2Model* model )
{
	return mMotionManager->updateParam( model->getModel() );
}

bool Live2DMotionQueueManager::isFinished()
{
	return mMotionManager->isFinished();
}

bool Live2DMotionQueueManager::isFinished( int motionQueueEntNo )
{
	return mMotionManager->isFinished( motionQueueEntNo );
}

void Live2DMotionQueueManager::stopAllMotions()
{
	mMotionManager->stopAllMotions();
}
