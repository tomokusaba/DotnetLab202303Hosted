
namespace DotnetLab202303Hosted.Server.Model {
    public class ParsonItem {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public long SkillId { get; set; }
    }

    public class SkillItem {
        public long Id { get; set; }
        public long SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public int Rare { get; set; }
    }

}
