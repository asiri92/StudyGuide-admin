using StudyGuide.DataAccess.Interfaces;
using StudyGuide_admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG = StudyGuide.Entities;

namespace StudyGuide_admin.Services
{
    public class StudyGuideService : IStudyGuideService
    {
        private readonly IStudyGuideRepository _studyGuideRepository;

        public StudyGuideService(IStudyGuideRepository studyGuideRepository) 
        {
            _studyGuideRepository = studyGuideRepository;
        }

        public void AddStudyGuide(SG.StudyGuide studyGuide)
        {
            _studyGuideRepository.AddStudyGuide(studyGuide);
        }

        public IEnumerable<SG.StudyGuide> GetStudyGuides()
        {
            return _studyGuideRepository.GetStudyGuides();
        }
    }
}