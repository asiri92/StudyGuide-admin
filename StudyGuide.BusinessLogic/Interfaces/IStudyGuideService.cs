using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using SG = StudyGuide.Entities;

namespace StudyGuide_admin.Interfaces
{
    public interface IStudyGuideService
    {
        IEnumerable<SG.StudyGuide> GetStudyGuides();
        void AddStudyGuide(SG.StudyGuide studyGuide);
    }
}
