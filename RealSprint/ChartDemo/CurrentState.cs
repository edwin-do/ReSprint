using System;

namespace ReSprint
{
	class CurrentState
	{
		public CurrentState() {
			string userName = "";
			string date = "";
			string fileName = "";
            string sampleName = "";
            float samplingRate = 1.0f;
			float sampleLength = 1.0f;
			float sampleWidth = 1.0f;

        }

/*		public displayUserInfo(string userName, string sampleName, string date, string fileName, float samplingRate, float sampleLength, float sampleWidth)
		{

		}*/

		public void updateUserInfo(string newUserInfo, int id)
		{
			switch(id)
			{
				case 0:
					this.userName = newUserInfo;
					break;
				case 1:
					this.date = newUserInfo;
					break;
				case 2:
					this.fileName = newUserInfo;
					break;
				default: break;
			}
		}

        //public void updateSampleInfo(string newSampleInfo, int id)
        //{
        //    switch (id)
        //    {
        //        case 0:
        //            this.samplingRate = float.parse(newSampleInfo);
        //            break;
        //        case 1:
        //            this.sampleName = newSampleInfo;
        //            break;
        //        case 2:
        //            this.sampleWidth = float.parse(newSampleInfo);
        //            break;
        //        case 3:
        //            this.sampleLength = float.parse(newSampleInfo);
        //            break;

        //        default: break;
        //    }
            //      }

            /*		public displayHardwareState()
                    {

                    }*/

            string userName;
        string date;
        string fileName;
        string sampleName;
        float samplingRate;
        float sampleLength;
        float sampleWidth;

    }
}
