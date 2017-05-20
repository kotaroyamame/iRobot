// エントリポイント
void RoombaMain()
{
	//CLEANボタンを押している間LEDを点灯する
	if(CleanButton == 1)
	{
		//LEDの点灯
		SetLEDColor('Y');
	}
	else//押していないとき
	{
		//LEDの消灯
        	SetLEDColor('N');
	}
}
