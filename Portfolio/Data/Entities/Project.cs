using System.ComponentModel.DataAnnotations;

namespace Portfolio.Data.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Görsel Url Boş Bırakılamaz.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Proje Adı Boş Bırakılamaz.")]
        [MinLength(3,ErrorMessage ="Proje Adı 3 Karakterden Kısa Olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proje Açıklaması Boş Bırakılamaz.")]
        [MaxLength(100,ErrorMessage ="Proje Açıklaması 100 Karakterden Fazla Olamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Github Url Boş Bırakılamaz.")]
        public string GithubUrl { get; set; }

        public List<ProjectTechStack>? ProjectTechStacks { get; set; }
    }
}
