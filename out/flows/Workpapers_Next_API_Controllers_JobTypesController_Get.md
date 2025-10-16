[web] GET /api/job-types/{id:Guid}  (Workpapers.Next.API.Controllers.JobTypesController.Get)  [L43–L51] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to JobTypeLightDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeLightDto) [L868]
  └─ calls JobTypeRepository.ReadQuery [L46]
  └─ query JobType [L46]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L46]
      └─ ... (no implementation details available)

