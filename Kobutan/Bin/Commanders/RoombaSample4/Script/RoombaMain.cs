// 状態を表す列挙型
enum State
{
    Run,        // 前進状態
    Back,       // 後進状態
    Turn,       // 旋回状態
}
// 状態変数
State state = State.Run;        // 状態
long changeStateTime = 0;       // 状態遷移時の経過時間

// 普段は直進し、バンパーが押されたら適当な時間旋回する
// このサンプルは、状態遷移を利用している
void RoombaMain()
{
    switch(state)
    {
        case State.Run:
            // バンパーが押されたら、後進状態に移行
            if ((RightBumper == 1) || (LeftBumper == 1))
            {
                Stop();
                state = State.Back;
                changeStateTime = ConnectTime;
            }
            // 通常は前進させておく
            else
            {
                Forward();
            }
            break;
        case State.Back:
            // 0.2秒後、旋回状態に移行
            if ((ConnectTime - changeStateTime) >= 200)
            {
                Stop();
                state = State.Turn;
                changeStateTime = ConnectTime;
            }
            // 後進中
            else
            {
                Backward();
            }
            break;
        case State.Turn:
            // 1秒後、前進状態に戻す
            if ((ConnectTime - changeStateTime) >= 1000)
            {
                Stop();
                state = State.Run;
            }
            // 旋回中
            else
            {
                TurnRight();
            }
            break;
        default:
            Stop();
            break;
    }

    // Exitを呼び出ださなければ、処理終了後に先頭に戻る
    //Exit();
}
