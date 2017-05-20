//======================================
// �ړ��R�}���h
//======================================
// Kobuki�Ɍ��X�p�ӂ���Ă��� Base Control �R�}���h�𒼐ڎg��
// speed  : ���x [mm/s] (-500 �` 500)
// radius : �Ȃ���Ԋu [mm] (-2000 �` 2000)
protected void BaseControl(short speed, short radius)
{
    OpCode = 137;
    SpeedParam = speed;
    if (radius == 0)
        RadiusParam = 0x8000;
    else
        RadiusParam = radius;
}

// �O�i
protected void Forward()
{
    BaseControl((short)(Speed * 5), 0);
}

// ��i
protected void Backward()
{
    BaseControl((short)(-(Speed * 5)), 0);
}

// ������
protected void TurnLeft()
{
    BaseControl((short)(TurnSpeed * 5), 1);
}

// �E����
protected void TurnRight()
{
    BaseControl((short)(TurnSpeed * 5), -1);
}

// �~�߂�
protected void Stop()
{
    BaseControl(0, 0);
}

// �O�i
protected void PWMControl(short rightpwm, short leftpwm)
{
    OpCode = 146;
    SpeedParam = rightpwm;
    RadiusParam = leftpwm;
}

//======================================
// �ݒ�R�}���h
//======================================
// �����̐ݒ� (0�`100)
protected void SetSpeed(int speed)
{
    // ���E�l�𒴂������͍̂ő�A�ŏ��l�ɍ��킹��
    if (speed > 100)
        speed = 100;
    if (speed < 0)
        speed = 0;
    // �p�����[�^�̐ݒ�
    Speed = speed;
}

// ���񑬓x�̐ݒ� (0�`100)
protected void SetTurnSpeed(int speed)
{
    // ���E�l�𒴂������͍̂ő�A�ŏ��l�ɍ��킹��
    if (speed > 100)
        speed = 100;
    if (speed < 0)
        speed = 0;
    // �p�����[�^�̐ݒ�
    TurnSpeed = speed;
}

// LED�̐F�ݒ� color�� N(����), R(��), G(��), Y(��)
protected void SetLEDColor(char color)
{
    // �F�̌���
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
// �T�E���h�R�}���h
//======================================
// ��
private void Sound(ushort note, byte duration)
{
    SoundFlg = true;
    SoundNote = note;
    SoundDuration = duration;
}

// �h�̉���炷
protected void Sound_DO()
{
    Sound(72, 0xff);
}

// �h#�̉���炷
protected void Sound_DO_Sharp()
{
    Sound(73, 0xff);
}

// ���̉���炷
protected void Sound_RE()
{
    Sound(74, 0xff);
}

// ��#�̉���炷
protected void Sound_RE_Sharp()
{
    Sound(75, 0xff);
}

// �~�̉���炷
protected void Sound_MI()
{
    Sound(76, 0xff);
}

// �t�@�̉���炷
protected void Sound_FA()
{
    Sound(77, 0xff);
}

// �t�@#�̉���炷
protected void Sound_FA_Sharp()
{
    Sound(78, 0xff);
}

// �\�̉���炷
protected void Sound_SO()
{
    Sound(79, 0xff);
}

// �\#�̉���炷
protected void Sound_SO_Sharp()
{
    Sound(80, 0xff);
}

// ���̉���炷
protected void Sound_RA()
{
    Sound(81, 0xff);
}

// ��#�̉���炷
protected void Sound_RA_Sharp()
{
    Sound(82, 0xff);
}

// �V�̉���炷
protected void Sound_SI()
{
    Sound(83, 0xff);
}

// �h�̉���炷
protected void Sound_DO_2()
{
    Sound(84, 0xff);
}

//======================================
// �V�X�e���R�}���h
//======================================
// �w�肵���~���b�ԃX���[�v
protected void Sleep(int ms)
{
    if (ms > 0)
        Thread.Sleep(ms);
}

// �v���O�������I������
protected void Exit()
{
    Stop();
    OnDataSending();
    iRobotMainThreadLoopFlg = false;
    throw new Exception();
}

// �ڑ�����
protected long ConnectTime { get { return Stopwatch.ElapsedMilliseconds; } }
