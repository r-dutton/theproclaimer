[web] GET /api/ui/workflow/job-filter-sets/{id:Guid}/can-i-save  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.CanISave)  [L75–L83] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.WriteQuery [L78]
  └─ write JobFilterSet [L78]
    └─ reads_from JobFilterSets
  └─ sends_request CanIEditJobFilterSet -> CanIEditJobFilterSetHandler [L82]
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
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobFilterSet writes=1 reads=0
    └─ requests 1
      └─ CanIEditJobFilterSet
    └─ handlers 1
      └─ CanIEditJobFilterSetHandler

