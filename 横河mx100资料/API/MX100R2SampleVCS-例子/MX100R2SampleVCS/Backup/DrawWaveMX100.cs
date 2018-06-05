using System;
using System.Drawing;
//using System.Collections;
//using System.ComponentModel;
using System.Windows.Forms;
//using System.Data;
using System.Text;
using System.Runtime.InteropServices;

public class DrawWaveMX100 : DAQMX100
{

	[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
	public static extern void Sleep(int dwMilliseconds);

	public const int RANGEV = 2;
	public const int fifoNo = 0;
	public const int INDEXNO = 0;

	protected int[] buf = new int[500];

	protected bool FlagDrawWave;

	public void DrawWave(PictureBox pic)
	{
		Pen whitepen = new Pen(Color.White);

		pic.CreateGraphics().Clear(Color.Black);
		int origp = pic.Height / 2;
		int xband = pic.Width / buf.Length;
		for (int i = 0; i < (buf.Length - 1); i++) 
		{
			int x1 = i * xband;
			int	x2 = (i + 1) * xband;
			int y1 = buf[i] + origp;
			int y2 = buf[i + 1] + origp;
			pic.CreateGraphics().DrawLine(whitepen, x1, y1, x2, y2);
		}
		whitepen.Dispose();
	}

	public void SetWave(int val, int point, PictureBox pic, Label vallabel)
	{
		byte[] strVal = new Byte[20];		
		Encoding enc = Encoding.GetEncoding ("ascii");

		int lenVal = toStringValueMX100(val, point, strVal, 20);
		vallabel.Text = enc.GetString(strVal);

		for (int i = 0; i < (buf.Length - 1); i++) 
		{
			buf[i] = buf[(i + 1)];
		}
		buf[(buf.Length - 1)] = (int)((-1) * toDoubleValueMX100(val, point) * (pic.Height / 2) / RANGEV);
	}

	public void RunDraw(TextBox address, PictureBox pic, Label vallabel)
	{
		int rc;

		FlagDrawWave = true;

		Encoding enc = Encoding.GetEncoding ("ascii");

		int comm = openMX100(enc.GetBytes(address.Text), out rc);
		int chNo = channelNumberMX100(comm, fifoNo, INDEXNO);

		rc = measStartMX100(comm);
		do 
		{
			rc = measDataFIFOMX100(comm, fifoNo);
			if (dataValidMX100(comm, chNo) == DAQMX100_VALID_ON) 
			{
				SetWave(dataValueMX100(comm, chNo), channelPointMX100(comm, chNo), pic, vallabel);
			} 
			else 
			{
				DrawWave(pic);
				Application.DoEvents();
				Sleep(100);
			}
		} while (FlagDrawWave);
		rc = measStopMX100(comm);
		rc = closeMX100(comm);
	}

	public void StopDraw()
	{
		FlagDrawWave = false;		
	}
}

