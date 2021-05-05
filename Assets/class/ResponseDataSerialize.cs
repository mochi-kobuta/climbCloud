using System;
using System.Collections.Generic;

namespace ResponseDataSerialize
{

    [Serializable]
    public class PositionDatas
    {
        // NOTE サーバー側で返している配列の変数名と合わせないとエラーになるっぽい
        public List<PositionData> data = new List<PositionData>();
    }

    [Serializable]
    public class PositionData
    {
        public int id;
        public float pos_x;
        public float pos_y;
        public float pos_z;

        public float scale_x;
        public float scale_y;
        public float scale_z;
    }
    
}
