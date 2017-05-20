



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


// エントリポイント
void RoombaMain()
{
    ImpactCalculator calculator1=new ImpactCalculator(1000,500);

    OutputString("変えるべき角度は"+calculator1.getAngle()+"度で、進むべき距離は"+calculator1.getDistance()+"です");
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
        Stop();
    Exit();
    // Encoder_Initialize(ref _LeftEnc, LeftEncoder);
    // Encoder_Initialize(ref _RightEnc, RightEncoder);
    // Encoder_Update(ref _LeftEnc, LeftEncoder);
    // Encoder_Update(ref _RightEnc, RightEncoder);
    // UpdateDistanceAngle();
    // //P14
    // OutputString("distance: " + _Distance);
    // BaseControl(100, 0);
    // while (_Distance < hoge.getDistance())
    // {
    //     Encoder_Update(ref _LeftEnc, LeftEncoder);
    //     Encoder_Update(ref _RightEnc, RightEncoder);
    //     UpdateDistanceAngle();
    //     OutputString("" + _LeftEnc.Value + " " + _RightEnc.Value);
    //     OutputString("distance: " + _Distance);
    //     Sleep(1);
    // }
    // Stop();
    // Exit();
}
