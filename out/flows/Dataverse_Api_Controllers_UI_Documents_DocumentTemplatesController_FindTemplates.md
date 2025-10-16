[web] GET /api/ui/documents/templates/  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.FindTemplates)  [L53–L87] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindDocumentTemplatesQuery -> FindDocumentTemplatesQueryHandler [L69]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentTemplatesQueryHandler.Handle [L57–L235]
      └─ uses_service UserService
        └─ method GetUserAsync [L80]
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
      └─ uses_service IControlledRepository<DocumentTemplate> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ FindDocumentTemplatesQuery
    └─ handlers 1
      └─ FindDocumentTemplatesQueryHandler

