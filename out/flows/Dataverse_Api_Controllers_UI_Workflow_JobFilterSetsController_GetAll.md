[web] GET /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetAll)  [L48–L63] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobFilterSetLightDto [L53]
    └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetLightDto) [L343]
  └─ calls JobFilterSetRepository.ReadQuery [L53]
  └─ query JobFilterSet [L53]
    └─ reads_from JobFilterSets
  └─ uses_service UserService
    └─ method GetUserId [L51]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobFilterSet writes=0 reads=1
    └─ services 1
      └─ UserService
    └─ mappings 1
      └─ JobFilterSetLightDto

