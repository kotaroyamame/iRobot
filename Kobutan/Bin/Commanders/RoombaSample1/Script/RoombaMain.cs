


//設定値
 const int X = 1000; // 001
 const int Y = 2000; // 010
//スピード
 const int SPEED = 30;
//旋回時のスピード
const int TURN_SPEED = 30;

//直進時の補正　-で右補正、＋で左補正
const int FORWARD_CORRECTION_FACTOR = 0;
//距離の補正　-で右補正、＋で左補正
const int DISTANCE_CORRECTION_FACTOR = 0;

void MaryHadALittleLamb(){
    int oneTempo=700;
    int oneEighth=(int)oneTempo/2;
    Sound_MI();
    Sleep(oneEighth*3);
    Sound_RE();
    Sleep(oneEighth*1);
    Sound_DO();
    Sleep(oneEighth*2);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*4);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_RE();
    Sleep(oneEighth*4);
    Sound_MI();
    Sleep(oneEighth*2);
    Sound_SO();
    Sleep(oneEighth*2);
    Sound_SO();
    Sleep(oneEighth*4);
    Sound_MI();
    Sleep(oneEighth*3);
    Sound_RE();
    Sleep(oneEighth*1);
    Sound_DO();
    Sleep(oneEighth*2);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*4);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_MI();
    Sleep(oneEighth*4);
    Sound_RE();
    Sleep(oneEighth*2);
    Sound_DO();
    Sleep(oneEighth*8);

}
class ImpactCalculator{
    private double distance=0;
    private int x;
    private int y;
    private int angle=0;
    private int pos=0;
    public ImpactCalculator(int x=0,int y=0){
        this.x=x;
        this.y=y;
        this.init();
    }
    private void init(){
        if(this.x>=0&&this.y>=0){
            this.pos=0;
        }else if(this.x>=0&&this.y<0){
            this.pos=1;

        }else if(this.x<0&&this.y<0){
            this.pos=2;
        }else if(this.x<0&&this.y>=0){
            this.pos=3;
        }
        this.distance=Math.Sqrt(System.Math.Pow(this.x,2)+System.Math.Pow(this.y,2));
        this.setAngle();
        
    }
    private void setAngle(){
        double angle=Math.Asin((double)this.y/this.distance);
        angle=(int)(angle*(180/Math.PI));
        switch(this.pos){
            case 0:
                this.angle=90-Math.Abs((int)Math.Truncate(angle));
            break;
            case 1:
                this.angle=Math.Abs((int)Math.Truncate(angle))+90;
            break;
            case 2:
                this.angle=90-Math.Abs((int)Math.Truncate(angle))+180;
            break;
            case 3:
                this.angle=Math.Abs((int)Math.Truncate(angle))+270;
            break;
        }
    }
    public double getDistance(){
        return this.distance;
        //keisan
    }
    public int getAngle(){
        return (int)this.angle;
        //keisan
    }
}

void DoDrive(int _distance){
    //距離に補正係数をかける
    int distance = _distance + DISTANCE_CORRECTION_FACTOR;
    
    Encoder_Initialize(ref _LeftEnc, LeftEncoder);
    Encoder_Initialize(ref _RightEnc, RightEncoder);
    Encoder_Update(ref _LeftEnc, LeftEncoder);
    Encoder_Update(ref _RightEnc, RightEncoder);
    UpdateDistanceAngle();
    //P14
    OutputString("distance: " + _Distance);
    BaseControl(100, 0);
    PWMControl(100+(FORWARD_CORRECTION_FACTOR), 100-(FORWARD_CORRECTION_FACTOR));
    while (_Distance < distance)
    {
        Encoder_Update(ref _LeftEnc, LeftEncoder);
        Encoder_Update(ref _RightEnc, RightEncoder);
        UpdateDistanceAngle();
        OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
        OutputString("distance: " + _Distance);
        Sleep(1);
    }
}

void DoturnRight(){
    Encoder_Initialize(ref _LeftEnc, LeftEncoder);
    Encoder_Initialize(ref _RightEnc, RightEncoder);
    Encoder_Update(ref _LeftEnc, LeftEncoder);
    Encoder_Update(ref _RightEnc, RightEncoder);
    UpdateDistanceAngle();
    //P14
    OutputString("distance: " + _Distance + "angle:" + _Angle);
    BaseControl(100, 10);
    while (_Angle < 90)
    {
        Encoder_Update(ref _LeftEnc, LeftEncoder);
        Encoder_Update(ref _RightEnc, RightEncoder);
        UpdateDistanceAngle();
        OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
        OutputString("distance: " + _Distance);
        Sleep(1);
    }
}

void Doturn(int angle){
    Encoder_Initialize(ref _LeftEnc, LeftEncoder);
    Encoder_Initialize(ref _RightEnc, RightEncoder);
    Encoder_Update(ref _LeftEnc, LeftEncoder);
    Encoder_Update(ref _RightEnc, RightEncoder);
    UpdateDistanceAngle();
    //P14
    OutputString("distance: " + _Distance + "angle:" + _Angle);
    //右ターンなら
    if(angle<180)
    {
        BaseControl(100, -1);
        while (_Angle < angle)
        {
            Encoder_Update(ref _LeftEnc, LeftEncoder);
            Encoder_Update(ref _RightEnc, RightEncoder);
            UpdateDistanceAngle();
            OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
            OutputString("_Angle: " + _Angle);
            Sleep(1);
        }
    }
    //左ターンなら
    else
    {
        BaseControl(100, 1);
        while (Math.Abs(_Angle) < (360-angle))
        {
            Encoder_Update(ref _LeftEnc, LeftEncoder);
            Encoder_Update(ref _RightEnc, RightEncoder);
            UpdateDistanceAngle();
            OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
            OutputString("_Angle: " + _Angle);
            Sleep(1);
        }
    }
    
    
}

void XYDrive(int distance,int angle){
    Doturn(angle);
    DoDrive(distance);
}

// エントリポイント
void RoombaMain()
{
    ImpactCalculator calculator1=new ImpactCalculator(X,Y);
    OutputString("変えるべき角度は"+calculator1.getAngle()+"度で、進むべき距離は"+calculator1.getDistance()+"です");
    XYDrive((int)calculator1.getDistance(),(int)calculator1.getAngle());
    // MaryHadALittleLamb();
    // DoDrive(1000);
    Stop();
    Exit();
}
