#pragma once

namespace live2d
{
	class Live2DModelWinGL;
	class Live2DMotion;
	class MotionQueueManager;
}

class Live2DWrapping
{
public:
	static void init();

	static void destory();
};

class Live2Model
{
public:
	Live2Model();
	~Live2Model();

	/*
	* @brief 
	* @param .moc ファイル
	*/
	void craeteModel( const char* str );

	/*!
	*  @brief  モデルを削除
	*/
	void deleteModel();
	/*
	* @brief 
	* @param texture番号
	* @param gltexture本体
	*/
	void setTexture( int textureNo, unsigned int glTexture );
	/*
	* @brief 
	* @param bool
	*/
	void setPremultipliedAlpha( bool r );
	/*
	* @brief 
	* @retval float - キャンバス幅
	*/
	float getCanvasWidth() const;
	/*
	* @brief 
	* @retval float - キャンバス高さ
	*/
	float getCanvasHeight() const;
	/*
	* @breif 
	* @param float 16 個 の Matrix
	*/
	void setMatrix( 
		float m00, float m01, float m02, float m03, 
		float m10, float m11, float m12, float m13, 
		float m20, float m21, float m22, float m23, 
		float m30, float m31, float m32, float m33 );
	/*
	* @brief 
	* @param int - パラメータインデックス
	* @param float - パラメータ値
	* @param float - ウェイト値( defualt = 1.0f )
	*/
	void setPrameterFloat( int index, float value, float wieght = 1.f );
	
	/*
	* @brief 
	* @param const char* - パラメータ名
	* @param float - パラメータ値
	* @param float - ウェイト値( defualt = 1.0f )
	*/
	void setPrameterFloat( const char* name, float value, float wieght = 1.f );

	/*!
	*  @brief:	   
	*  @return:   void
	*/
	void update();
	
	/*!
	*  @brief:	  
	*  @return:   void
	*/
	void draw();

	/*!
	*  @brief:	  
	*  @return:   const live2d::Live2DModelWinGL*
	*/
	live2d::Live2DModelWinGL* getModel() const;

private:
	live2d::Live2DModelWinGL*	mLive2DModel;
};

class Live2DAnimation
{
public:
	Live2DAnimation();
	~Live2DAnimation();
	
public:
	/*!
	*  @brief:	  
	*  @param: 	  const char * filepath
	*  @return:   void
	*/
	void loadMotion( const char* filepath );

	/*!
	*  @brief  モーションを解放
	*/
	void deleteMotion();

	/*!
	*  @brief:	  
	*  @param: 	  live2d::ALive2DModel * model
	*  @param: 	  long long timeMSec
	*  @param: 	  float weight
	*  @param: 	  MotionQueueEnt * motionQueueEnt
	*  @return:   void
	*/
	void updateParamExe( Live2Model* model, long long timeMSec, float weight );

	/*!
	*  @brief:	 
	*  @param: 	  bool loop
	*  @return:   void
	*/
	void setLoop( bool loop );

	/*!
	*  @brief:	  
	*  @return:   bool
	*/
	bool isLoop();

	/*!
	*  @brief:	  
	*  @param: 	  bool loopFadeIn
	*  @return:   void
	*/
	void setLoopFadeIn( bool loopFadeIn );

	/*!
	*  @brief:	  
	*  @return:   bool
	*/
	bool isLoopFadeIn();

	/*!
	*  @brief:	   
	*  @return:   live2d::Live2DMotion*
	*/
	live2d::Live2DMotion* getMotion() const;
	
private:
	live2d::Live2DMotion* mMotion;
};

class Live2DMotionQueueManager
{
public:
	Live2DMotionQueueManager();

	~Live2DMotionQueueManager();

public:
	/*!
	*  @brief モーション管理解放
	*/
	void deleteMotionManager();
	/*!
	*  @brief:	   
	*  @param: 	  AMotion * motion
	*  @param: 	  bool autoDelete
	*  @return:   int
	*/
	int startMotion( Live2DAnimation* motion, bool autoDelete );

	/*!
	*  @brief:	   
	*  @param: 	  live2d::ALive2DModel * model
	*  @return:   bool
	*/
	bool updateParam( Live2Model* model );

	/*!
	*  @brief:	   
	*  @return:   bool
	*/
	bool isFinished();
	
	/*!
	*  @brief:	   
	*  @param: 	  int motionQueueEntNo
	*  @return:   bool
	*/
	bool isFinished( int motionQueueEntNo );

	/*!
	*  @brief:	   
	*  @return:   void
	*/
	void stopAllMotions();
private:
	live2d::MotionQueueManager* mMotionManager;
};