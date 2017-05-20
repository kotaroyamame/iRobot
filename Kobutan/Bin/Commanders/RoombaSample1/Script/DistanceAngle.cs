// �G���R�[�_
Encoder _RightEnc;    // �E�G���R�[�_
Encoder _LeftEnc;     // ���G���R�[�_

//======================================================
// �G���R�[�_
//
// �T�v:
//   ���̃G���R�[�_�l�����̂܂܎g����2�o�C�g�������Ȃ����߂������ӂ��Ƃ���������A
//   �r���ŃG���R�[�_�l�����Z�b�g���邱�Ƃ��ł��Ȃ��Ƃ�������肪����B
//   ���̂��߁A�G���R�[�_�l��int�^�Ɋg�����A���Z�b�g�@�\���ǉ������B
//======================================================
// �萔
const short OVERFLOW = (short)(ushort.MaxValue / 2);     // �I�[�o�[�t���[���m�̋��E�l

// �G���R�[�_�l���i�[����\����
// �\���̂̒�`�ł́A�e�v�f��public��t����(�Ƃ肠�������͂��܂��Ȃ��Ǝv���Ă�������)
struct Encoder
{
    // ���݂̒l
    public int Value;
    // �O��̓��͒l
    public ushort LastInput;
}

// �G���R�[�_�l�̍X�V
//   encoder: �G���R�[�_�l���i�[����\���̂̃|�C���^ (�|�C���^�n���ɂ� ref��t����)
//   value:   ���̃G���R�[�_�l
void Encoder_Update(ref Encoder encoder, ushort value)
{
    // �O��̓��͒l����̍������v�Z
    int offset = (int)(value - encoder.LastInput);
    // �I�[�o�[�t���[�̌��m�ƏC��
    if (offset >= OVERFLOW)
    {
        offset = (int)(-(ushort.MaxValue - offset + 1));
    }
    else if (offset <= -OVERFLOW)
    {
        offset = (int)(ushort.MaxValue + offset + 1);
    }

    // �����𔽉f
    encoder.Value += offset;

    // ����̂��߂Ɍ��݂̓��͒l���m��
    encoder.LastInput = value;
}

// �G���R�[�_�l�̏�����
//   encoder: �G���R�[�_�l���i�[����\���̂̃|�C���^ (�|�C���^�n���ɂ� ref��t����)
//   value:   ���̃G���R�[�_�l
void Encoder_Initialize(ref Encoder encoder, ushort value)
{
    encoder.LastInput = value;
    encoder.Value = 0;
}

//======================================================
// ����,�p�x
//======================================================
int _Distance = 0;
int _Angle = 0;

void UpdateDistanceAngle()
{
    double r = ((_RightEnc.Value * 72.0 * 3.141592) / 508.8);
    double l = ((_LeftEnc.Value * 72.0 * 3.141592) / 508.8);
    _Distance = (int)((r + l) / 2);
    _Angle = (int)(((r - l) * 235 * 2 * 3.141592) / 360);
}
