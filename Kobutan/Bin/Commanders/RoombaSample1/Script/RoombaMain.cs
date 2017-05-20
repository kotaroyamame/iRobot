



class Hoge{
    private double distance=0;
    private int x;
    private int y;
    private int angle=0;
   public Hoge(int x=0,int y=0){
        this.x=x;
        this.y=y;
        this.init();
    }
    private void init(){
        this.distance=Math.Sqrt(System.Math.Pow(this.x,2)+System.Math.Pow(this.y,2));
        double angle=Math.Asin((double)this.y/this.distance);
        this.angle=(int)(angle*(180/Math.PI));
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


// エントリポイント
void RoombaMain()
{

    // int distance=0;
    // distance = xy[0]*xy[0]+xy[1]*xy[1];
    Hoge hoge=new Hoge(1000,1000);

    OutputString("角度は"+hoge.getDistance());
    // int angle=0;
    // while(angle<=hoge.getAngle()){
    //     anglee+=Angle;
    //     OutputInteger(Angle);
    //     Sleep(1);
    // }
    // // BaseControl(100,0);
    // int distance=0;
    // while (distance<hoge.getDistance()){
    //     distance+=Distance;
    //     OutputInteger(Distance);
    //     Sleep(1);
    // }
    
    Encoder_Initialize(ref _LeftEnc, LeftEncoder);
    Encoder_Initialize(ref _RightEnc, RightEncoder);
    Encoder_Update(ref _LeftEnc, LeftEncoder);
    Encoder_Update(ref _RightEnc, RightEncoder);
    UpdateDistanceAngle();
    //P14
    OutputString("distance: " + _Distance);
    BaseControl(100, 0);
    while (_Distance < hoge.getDistance())
    {
        Encoder_Update(ref _LeftEnc, LeftEncoder);
        Encoder_Update(ref _RightEnc, RightEncoder);
        UpdateDistanceAngle();
        OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
        OutputString("distance: " + _Distance);
        Sleep(1);
    }
    Stop();
    Exit();
}
