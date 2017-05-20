// iRobotMainスレッド
private void iRobotMainThread()
{
    // 接続が切られるまでループ
    while (iRobotMainThreadLoopFlg)
    {
        try
        {
            RoombaMain();
        }
        catch (Exception) { }

        Sleep(iRobotMainThreadSycle);
    }
}

// 受信データを入力
private int _ReceiveState = 0;
private int _ReceiveStateCounter = 0;
private byte[] _SensorBuf = new byte[256];
private byte[] _ReceiveBuf = new byte[256];
//距離補正値
private short ReceiveDistance = 0;
private int CorrectionValueOfDistance = -1;
private void InputReceivedData(byte data)
{
    switch (_ReceiveState)
    {
        case 0:
            if (data == 19)
            {
                _SensorBuf[0] = data;
                _ReceiveState = 1;
            }
            break;
        case 1:
            _SensorBuf[1] = data;
			_ReceiveStateCounter = 0;
            _ReceiveState = 2;
            break;
        case 2:
			int index = 2 + _ReceiveStateCounter;
            _SensorBuf[index] = data;
            if(_ReceiveStateCounter >= _SensorBuf[1])
			{
				int checkSum = 0;
				for(int j = 0; j < _SensorBuf[1]; ++j)
				{
					checkSum += _SensorBuf[2 + j];
				}
				for (int j = 0; j < _SensorBuf[1]; )
				{
					int index2 = 2 + j;
					switch (_SensorBuf[index2])
					{
						case 7:
							RightBumper = ((_SensorBuf[index2 + 1] & 0x01) != 0 ? 1 : 0);
                            LeftBumper = ((_SensorBuf[index2 + 1] & 0x02) != 0 ? 1 : 0);
                            RightWheelDrop = ((_SensorBuf[index2 + 1] & 0x04) != 0 ? 1 : 0);
                            LeftWheelDrop = ((_SensorBuf[index2 + 1] & 0x08) != 0 ? 1 : 0);
                            j += 2;
                            break;
                        case 17:
                            DockIRdata = _SensorBuf[index2 + 1];
                            j += 2;
                            break;
                        case 18:
                            CleanButton = ((_SensorBuf[index2 + 1] & 0x01) != 0 ? 1 : 0);
                            SpotButton = ((_SensorBuf[index2 + 1] & 0x02) != 0 ? 1 : 0);
                            DockButton = ((_SensorBuf[index2 + 1] & 0x04) != 0 ? 1 : 0);
                            MinuteButton = ((_SensorBuf[index2 + 1] & 0x08) != 0 ? 1 : 0);
                            HourButton = ((_SensorBuf[index2 + 1] & 0x16) != 0 ? 1 : 0);
                            DayButton = ((_SensorBuf[index2 + 1] & 0x32) != 0 ? 1 : 0);
                            ScheduleButton = ((_SensorBuf[index2 + 1] & 0x64) != 0 ? 1 : 0);
                            ClockButton = ((_SensorBuf[index2 + 1] & 0x128) != 0 ? 1 : 0);
                            j += 2;
                            break;
                        case 19:
                            ReceiveDistance = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            //_SensorData.Distance += (double)(distance * CorrectionValueOfDistance);
                            ReceiveDistance = (short)(ReceiveDistance * CorrectionValueOfDistance);
                            Distance += ReceiveDistance;
                            j += 3;
                            break;
                        case 20:
                            Angle = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            //_SensorData.Angle += (double)(angle * CorrectionValueOfAngle);
                            //_SensorData.TemporarilyAngle = (double)(angle * CorrectionValueOfAngle);
                            j += 3;
                            break;
                        case 27:
                            WallSignal = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 28:
                            CliffLeftSignal = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 29:
                            CliffFrontLeftSignal = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 30:
                            CliffFrontRightSignal = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 31:
                            CliffRightSignal = (short)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 34:
                            Charger = _SensorBuf[index2 + 1];
                            j += 2;
                            break;
                        case 43:
                            LeftEncoder = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            //_SensorData.EncoderLeftDistance = (double)_SensorData.LeftEncoderCounts * CorrectionValueOfDistance;
                            j += 3;
                            break;
                        case 44:
                            RightEncoder = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            //_SensorData.EncoderRightDistance = (double)_SensorData.RightEncoderCounts * CorrectionValueOfDistance;
                            j += 3;
                            break;
                        case 46:
                            LightBumpLeft = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 47:
                            LightBumpFrontLeft = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 48:
                            LightBumpCenterLeft = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 49:
                            LightBumpCenterRight = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 50:
                            LightBumpFrontRight = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
                        case 51:
                            LightBumpRight = (ushort)((_SensorBuf[index2 + 1] << 8) | _SensorBuf[index2 + 2]);
                            j += 3;
                            break;
						default:
							++j;
							break;
					}
				}
				_ReceiveState = 0;
			}
			++_ReceiveStateCounter;
            break;
		default:
			_ReceiveState = 0;
			break;
    }
}

// チェックサムを計算するメソッド
private byte CalcChecksum(byte[] buf, int index)
{
    byte sum = 0;
    for(int i = 1; i < index; ++i)
    {
        sum += buf[i];
    }
    return sum;
}
