// �A�N�e�B�x�[�g���Ɏ��s����郁�\�b�h
public override void OnActivated()
{
}

// �f�B�A�N�e�B�x�[�g���Ɏ��s����郁�\�b�h
public override void OnDeactivating()
{
}

// �ڑ����Ɏ��s����郁�\�b�h
public override void OnConnected()
{
    // �X�^�[�g��m�点��R�}���h�𑗂�
    ObjectBase.CommandManager.SendBuf[0] = (byte)128;
    ObjectBase.CommandManager.SendBuf[1] = 0;
    ObjectBase.CommandManager.SendBuf[2] = 0;
    ObjectBase.CommandManager.SendBuf[3] = 0;
    ObjectBase.CommandManager.SendBuf[4] = 0;
    ObjectBase.CommandManager.SendBuf[5] = 0;
    ObjectBase.CommandManager.SendBuf[6] = 0;
    ObjectBase.CommandManager.SendBuf[7] = 0;
    ObjectBase.CommandManager.Send(0, 8);
    Sleep(1000);
    ObjectBase.CommandManager.SendBuf[0] = (byte)132;
    ObjectBase.CommandManager.Send(0, 8);
    Sleep(1000);
    // �Z���T�l�̗v��
    
    ObjectBase.CommandManager.SendBuf[0] = (byte)148;
    ObjectBase.CommandManager.SendBuf[1] = (byte)20;
    ObjectBase.CommandManager.SendBuf[2] = (byte)7;
    ObjectBase.CommandManager.SendBuf[3] = (byte)17;
    ObjectBase.CommandManager.SendBuf[4] = (byte)18;
    ObjectBase.CommandManager.SendBuf[5] = (byte)19;
    ObjectBase.CommandManager.SendBuf[6] = (byte)20;
    ObjectBase.CommandManager.SendBuf[7] = (byte)27;
    ObjectBase.CommandManager.SendBuf[8] = (byte)28;
    ObjectBase.CommandManager.SendBuf[9] = (byte)29;
    ObjectBase.CommandManager.SendBuf[10] = (byte)30;
    ObjectBase.CommandManager.SendBuf[11] = (byte)31;
    ObjectBase.CommandManager.SendBuf[12] = (byte)34;
    ObjectBase.CommandManager.SendBuf[13] = (byte)43;
    ObjectBase.CommandManager.SendBuf[14] = (byte)44;
    ObjectBase.CommandManager.SendBuf[15] = (byte)45;
    ObjectBase.CommandManager.SendBuf[16] = (byte)46;
    ObjectBase.CommandManager.SendBuf[17] = (byte)47;
    ObjectBase.CommandManager.SendBuf[18] = (byte)48;
    ObjectBase.CommandManager.SendBuf[19] = (byte)49;
    ObjectBase.CommandManager.SendBuf[20] = (byte)50;
    ObjectBase.CommandManager.SendBuf[21] = (byte)51;
    ObjectBase.CommandManager.Send(0, 22);
    Sleep(500);
    // �T���v���X���b�h
    iRobotMainThreadReference = new Thread(new ThreadStart(iRobotMainThread));
    iRobotMainThreadReference.IsBackground = true;
    iRobotMainThreadLoopFlg = true;
    iRobotMainThreadReference.Start();
    // �X�g�b�v�E�H�b�`�̃X�^�[�g
    Stopwatch.Reset();
    Stopwatch.Start();
}

// �ؒf���Ɏ��s����郁�\�b�h
public override void OnDisconnecting()
{
    ObjectBase.CommandManager.SendBuf[0] = (byte)133;
    ObjectBase.CommandManager.Send(0, 1);
    // �T���v���X���b�h�̏I��
    iRobotMainThreadReference.Abort();
    Stop();
    OnDataSending();
    iRobotMainThreadLoopFlg = false;
    // �X�g�b�v�E�H�b�`�̒�~
    Stopwatch.Stop();
    Stopwatch.Reset();
}

// �f�[�^�𑗂�^�C�~���O���������Ɏ��s����郁�\�b�h
public override void OnDataSending()
{
    // Exit ����ĂȂ���Α���
    if (iRobotMainThreadLoopFlg)
    {
        // Drive
        ObjectBase.CommandManager.SendBuf[0] = (byte)OpCode;
        ObjectBase.CommandManager.SendBuf[1] = (byte)((SpeedParam & 0xff00) >> 8);
        ObjectBase.CommandManager.SendBuf[2] = (byte)(SpeedParam & 0x00ff);
        ObjectBase.CommandManager.SendBuf[3] = (byte)((RadiusParam & 0xff00) >> 8);
        ObjectBase.CommandManager.SendBuf[4] = (byte)(RadiusParam & 0x00ff);
        ObjectBase.CommandManager.SendBuf[5] = 0;
        ObjectBase.CommandManager.SendBuf[6] = 0;
        ObjectBase.CommandManager.SendBuf[7] = 0;
        // ���M
        ObjectBase.CommandManager.Send(0, 8);

        // LED
        ObjectBase.CommandManager.SendBuf[0] = 139;
        ObjectBase.CommandManager.SendBuf[1] = (byte)6;
        ObjectBase.CommandManager.SendBuf[2] = (byte)LEDParam;
        ObjectBase.CommandManager.SendBuf[3] = (byte)LEDIntensity;

        // ���M
        ObjectBase.CommandManager.Send(0, 4);
    }

    if (SoundFlg)
    {
        //���̃Z�b�g
        ObjectBase.CommandManager.SendBuf[0] = (byte)140;
        ObjectBase.CommandManager.SendBuf[1] = (byte)1;
        ObjectBase.CommandManager.SendBuf[2] = (byte)1;
        ObjectBase.CommandManager.SendBuf[3] = (byte)SoundNote;
        ObjectBase.CommandManager.SendBuf[4] = (byte)SoundDuration;
        // ���M
        ObjectBase.CommandManager.Send(0, 5);
        //Sleep(50);
        //���̍Đ�
        ObjectBase.CommandManager.SendBuf[0] = (byte)141;
        ObjectBase.CommandManager.SendBuf[1] = (byte)1;
        // ���M
        ObjectBase.CommandManager.Send(0, 2);
        Sleep(500);
        SoundFlg = false;
    }

}

// �f�[�^����M���ꂽ���Ɏ��s����郁�\�b�h
public override void OnDataReceived()
{
    
    // ��M�f�[�^�̓ǂݍ���
    int size = ObjectBase.CommandManager.BytesToRead;
    ObjectBase.CommandManager.Receive(0, size);
    for (int i = 0; i < size; ++i)
    {
        // ��M�f�[�^�����
        InputReceivedData(ObjectBase.CommandManager.ReceiveBuf[i]);
    }
}
