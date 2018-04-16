
using DelimitedStringParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDDataMorpher
{
    [Delimiter('\t')]
    class CSDRawModel
    {
        [FieldId(0)]
        public Int32 AssetId { get; set; }
        [FieldId(1)]
        public Int32 PointId { get; set; }
        [FieldId(2)]
        public string PointName { get; set; }
        [FieldId(3)]
        public string TimeStamp { get; set; }
        [FieldId(4)]
        public double Value { get; set; }
    }
}
