//======================================
// 移動コマンド
//======================================
// Kobukiに元々用意されている Base Control コマンドを直接使う
// speed  : 速度 [mm/s] (-500 ～ 500)
// radius : 曲がる間隔 [mm] (-2000 ～ 2000)
protected void BaseControl(short speed, short radius)
{
    OpCode = 137;
    SpeedParam = speed;
    if (radius == 0)
        RadiusParam = 0x8000;
    else
        RadiusParam = radius;
}

// 前進
protected void Forward()
{
    BaseControl((short)(Speed * 5), 0);
}

// 後進
protected void Backward()
{
    BaseControl((short)(-(Speed * 5)), 0);
}

// 左旋回
protected void TurnLeft()
{
    BaseControl((short)(TurnSpeed * 5), 1);
}

// 右旋回
protected void TurnRight()
{
    BaseControl((short)(TurnSpeed * 5), -1);
}

// 止める
protected void Stop()
{
    BaseControl(0, 0);
}

// 前進
protected void PWMControl(short rightpwm, short leftpwm)
{
    OpCode = 146;
    SpeedParam = rightpwm;
    RadiusParam = leftpwm;
}

//======================================
// 設定コマンド
//======================================
// 速さの設定 (0～100)
protected void SetSpeed(int speed)
{
    // 境界値を超えたものは最大、最小値に合わせる
    if (speed > 100)
        speed = 100;
    if (speed < 0)
        speed = 0;
    // パラメータの設定
    Speed = speed;
}

// 旋回速度の設定 (0～100)
protected void SetTurnSpeed(int speed)
{
    // 境界値を超えたものは最大、最小値に合わせる
    if (speed > 100)
        speed = 100;
    if (speed < 0)
        speed = 0;
    // パラメータの設定
    TurnSpeed = speed;
}

// LEDの色設定 colorは N(無し), R(赤), G(緑), Y(黄)
protected void SetLEDColor(char color)
{
    // 色の決定
    if (color == 'N')
        LEDIntensity = 0;
    if (color == 'R')
        LEDIntensity = 255;
        LEDParam = 255;
    if (color == 'G')
        LEDIntensity = 255;
        LEDParam = 1;
    if (color == 'Y')
        LEDIntensity = 255;
        LEDParam = 128;
}

//======================================
// サウンドコマンド
//======================================
// 音
private void Sound(ushort note, byte duration)
{
    SoundFlg = true;
    SoundNote = note;
    SoundDuration = duration;
}

// ドの音を鳴らす
protected void Sound_DO()
{
    Sound(72, 0xff);
}

// ド#の音を鳴らす
protected void Sound_DO_Sharp()
{
    Sound(73, 0xff);
}

// レの音を鳴らす
protected void Sound_RE()
{
    Sound(74, 0xff);
}

// レ#の音を鳴らす
protected void Sound_RE_Sharp()
{
    Sound(75, 0xff);
}

// ミの音を鳴らす
protected void Sound_MI()
{
    Sound(76, 0xff);
}

// ファの音を鳴らす
protected void Sound_FA()
{
    Sound(77, 0xff);
}

// ファ#の音を鳴らす
protected void Sound_FA_Sharp()
{
    Sound(78, 0xff);
}

// ソの音を鳴らす
protected void Sound_SO()
{
    Sound(79, 0xff);
}

// ソ#の音を鳴らす
protected void Sound_SO_Sharp()
{
    Sound(80, 0xff);
}

// ラの音を鳴らす
protected void Sound_RA()
{
    Sound(81, 0xff);
}

// ラ#の音を鳴らす
protected void Sound_RA_Sharp()
{
    Sound(82, 0xff);
}

// シの音を鳴らす
protected void Sound_SI()
{
    Sound(83, 0xff);
}

// ドの音を鳴らす
protected void Sound_DO_2()
{
    Sound(84, 0xff);
}

//======================================
// システムコマンド
//======================================
// 指定したミリ秒間スリープ
protected void Sleep(int ms)
{
    if (ms > 0)
        Thread.Sleep(ms);
}

// プログラムを終了する
protected void Exit()
{
    Stop();
    OnDataSending();
    iRobotMainThreadLoopFlg = false;
    throw new Exception();
}

// 接続時間
protected long ConnectTime { get { return Stopwatch.ElapsedMilliseconds; } }
