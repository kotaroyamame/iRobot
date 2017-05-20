



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
        this.angle=angle*(180/Math.PI);
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
}


// エントリポイント
void RoombaMain()
{

    int xy[2]={-50,50};

    // int distance=0;
    // distance = xy[0]*xy[0]+xy[1]*xy[1];
    Hoge hoge=new Hoge(xy[0],xy[1]);

    OutputString("角度は"+hoge.getDistance());
    int angle=0;
    while(angle<=hoge.getAngle()){
        anglee+=Angle;
        OutputInteger(Angle);
        Sleep(1);
    }
    // BaseControl(100,0);
    int distance=0;
    while (distance<hoge.getDistance()){
        distance+=Distance;
        OutputInteger(Distance);
        Sleep(1);
    }
    
    
    
    // OutputString("START!");
    // SetSpeed(8);
    // OutputString("setSpeed");
    // // 1秒間 前進
    // Forward();        // 前進
    Sleep(3000);      // 次のステップに移るまで1秒待つ
    Stop();           // ストップ
    // // 2.5秒間 左旋回
    // TurnRight();       // 左旋回
    // Sleep(5500);      // 次のステップに移るまで2.5秒待つ
    // Stop();           // ストップ
    // // 1秒間 前進
    // Forward();        // 前進
    // Sleep(1000);      // 次のステップに移るまで1秒待つ
    // Stop();           // ストップ
    // OutputString("STOP!!");
    Exit();
}
