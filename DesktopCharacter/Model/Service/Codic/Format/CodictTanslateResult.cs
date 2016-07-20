using System.Collections.Generic;

namespace DesktopCharacter.Model.Service.Codic.Format
{
    public class Candidate
    {
        public string text { get; set; }
        public string text_in_casing { get; set; }
    }

    public class Word
    {
        public bool successful { get; set; }
        public string text { get; set; }
        public string translated_text { get; set; }
        public List<Candidate> candidates { get; set; }
    }

    public class CodictTanslateResult
    {
        public bool successful { get; set; }
        public string text { get; set; }
        public string translated_text { get; set; }
        public List<Word> words { get; set; }
    }
}
