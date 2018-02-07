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
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Emailo = v.ContactEmailo, Phone = v.ContactPhone }))
                .ForMember(vr => vr.VehicleFeature, opt => opt.MapFrom(v => v.VehicleFeature.Select(vf => vf.FeatureId)));

            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmailo, opt => opt.MapFrom(vr => vr.Contact.Emailo))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.VehicleFeature, opt => opt.MapFrom(vr => vr.VehicleFeature.Select(id => new VehicleFeature { FeatureId = id })));
        }
    }
}
