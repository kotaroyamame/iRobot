// エンコーダ
Encoder _RightEnc;    // 右エンコーダ
Encoder _LeftEnc;     // 左エンコーダ

//======================================================
// エンコーダ
//
// 概要:
//   生のエンコーダ値をそのまま使うと2バイト分しかないためすぐあふれるといった問題や、
//   途中でエンコーダ値をリセットすることができないといった問題がある。
//   そのため、エンコーダ値をint型に拡張し、リセット機能も追加した。
//======================================================
// 定数
const short OVERFLOW = (short)(ushort.MaxValue / 2);     // オーバーフロー検知の境界値

// エンコーダ値を格納する構造体
// 構造体の定義では、各要素にpublicを付ける(とりあえず今はおまじないと思ってください)
struct Encoder
{
    // 現在の値
    public int Value;
    // 前回の入力値
    public ushort LastInput;
}

// エンコーダ値の更新
//   encoder: エンコーダ値を格納する構造体のポインタ (ポインタ渡しには refを付ける)
//   value:   生のエンコーダ値
void Encoder_Update(ref Encoder encoder, ushort value)
{
    // 前回の入力値からの差分を計算
    int offset = (int)(value - encoder.LastInput);
    // オーバーフローの検知と修正
    if (offset >= OVERFLOW)
    {
        offset = (int)(-(ushort.MaxValue - offset + 1));
    }
    else if (offset <= -OVERFLOW)
    {
        offset = (int)(ushort.MaxValue + offset + 1);
    }

    // 差分を反映
    encoder.Value += offset;

    // 次回のために現在の入力値を確保
    encoder.LastInput = value;
}

// エンコーダ値の初期化
//   encoder: エンコーダ値を格納する構造体のポインタ (ポインタ渡しには refを付ける)
//   value:   生のエンコーダ値
void Encoder_Initialize(ref Encoder encoder, ushort value)
{
    encoder.LastInput = value;
    encoder.Value = 0;
}

//======================================================
// 距離,角度
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
