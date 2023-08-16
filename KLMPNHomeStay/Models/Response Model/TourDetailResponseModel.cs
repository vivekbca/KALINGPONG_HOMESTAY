using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class TourDetailResponseModel
    {
        public string tourId { get; set; }
        public string tourName { get; set; }
        public string destination { get; set; }
        public int destinationDay { get; set; }
        public int destinationNight { get; set; }
        public int cost { get; set; }
        public string contactPersonName { get; set; }
        public int? contactPersonMobile { get; set; }
        public string contactPersonEmail { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public string img5 { get; set; }
        public string facility1Id { get; set; }
        public string facility1Name { get; set; }
        public string facility1Img { get; set; }
        public string facility2Id { get; set; }
        public string facility2Name { get; set; }
        public string facility2Img { get; set; }
        public string facility3Id { get; set; }
        public string facility3Name { get; set; }
        public string facility3Img { get; set; }
        public string facility4Id { get; set; }
        public string facility4Name { get; set; }
        public string facility4Img { get; set; }
        public string facility5Id { get; set; }
        public string facility5Name { get; set; }
        public string facility5Img { get; set; }
        public string tourPDFFile { get; set; }
        public List<string> facilities = new List<string>();
        public List<HomestayAminitiestesting> HSAmImg { get; set; }
    }
}
