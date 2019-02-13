using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public enum extrapolate_type
    {
        constant,
        linear,
        cycle,
        cycle_offset,
        bounce,
    }
    public enum tangent_type
    {
        flat,
        linear,
        smooth,
    }
    public class KeyFrame
    {
        public float time, value, tan_in_val, tan_out_val, a, b, c, d;
        public tangent_type tangent_in, tangent_out;
    }
    public class Channel
    {
        public extrapolate_type extrap_in, extrap_out;
        public List<KeyFrame> keyList;
    }
    public class OneJointAnim
    {
        public Channel channel_x, channel_y, channel_z;
    }
}
