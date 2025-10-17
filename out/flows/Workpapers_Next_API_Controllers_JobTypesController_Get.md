[web] GET /api/job-types/{id:Guid}  (Workpapers.Next.API.Controllers.JobTypesController.Get)  [L43–L51] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to JobTypeLightDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeLightDto) [L868]
  └─ calls JobTypeRepository.ReadQuery [L46]
  └─ query JobType [L46]
    └─ reads_from JobTypes
  └─ uses_service JobTypeRepository
    └─ method ReadQuery [L46]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.JobTypeRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ services 1
      └─ JobTypeRepository
    └─ mappings 1
      └─ JobTypeLightDto

