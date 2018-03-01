using AutoMapper;
using CursoUdemy.Models;
using CursoUdemy.Resources;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoUdemy.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Emailo = v.ContactEmailo, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Emailo = v.ContactEmailo, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new FeatureResource { Id = vf.Feature.Id, Name = vf.Feature.Name })));

            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmailo, opt => opt.MapFrom(vr => vr.Contact.Emailo))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {

                    var removeFeactures = v.Features
                    .Where(f => !vr.Features
                    .Contains(f.FeatureId)).ToList();
                    foreach (var f in removeFeactures)
                        v.Features.Remove(f);

                    var addFeacture = vr.Features
                    .Where(id => !v.Features.Any(f => f.FeatureId == id))
                    .Select(id => new VehicleFeature { FeatureId = id }).ToList();
                    foreach (var f in addFeacture)
                        v.Features.Add(f);
            });
        }
    }
}
