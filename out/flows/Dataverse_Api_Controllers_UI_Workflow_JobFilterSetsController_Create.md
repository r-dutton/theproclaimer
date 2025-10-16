[web] POST /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Create)  [L108–L116] status=201 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.Add [L114]
  └─ insert JobFilterSet [L114]
    └─ reads_from JobFilterSets
  └─ uses_service UserService
    └─ method GetUserId [L111]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ sends_request CanIEditJobFilterSet -> CanIEditJobFilterSetHandler [L112]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                      └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L82]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                      └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L71]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L81]
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobFilterSet writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ CanIEditJobFilterSet
    └─ handlers 1
      └─ CanIEditJobFilterSetHandler

