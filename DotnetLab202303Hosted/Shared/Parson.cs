using DotnetLab202303Hosted.Shared.CustomValidate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetLab202303Hosted.Shared
{
    public class Parson
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "名前は必ず入力してください。")]
        public string Name { get; set; } = string.Empty;
        [BirthdayValidator(ErrorMessage = "誕生日は今日以前の日付を入力してください。")]
        public DateTime Birthday { get; set; } = DateTime.Now;
        [ValidateComplexType]
        public List<Skill> Skills { get; set; } = new();
    }

    public class Skill
    {
        [Required(ErrorMessage = "スキル名は必ず入力してください。")]
        [StringLength(30)]
        public string SkillName { get; set; } = string.Empty;
        [Range(1, 5, ErrorMessage = "レア度は1から5の数値を入力してください。")]
        public int Rare { get; set; }
    }
}
