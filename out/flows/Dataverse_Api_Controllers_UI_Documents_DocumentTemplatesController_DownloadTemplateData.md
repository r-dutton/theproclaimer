[web] GET /api/ui/documents/templates/{templateId:guid}/download  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.DownloadTemplateData)  [L282–L288] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDocumentTemplateDataQuery -> GetDocumentTemplateDataQueryHandler [L287]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentTemplateDataQueryHandler.Handle [L30–L95]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L75]
          └─ implementation DocumentServiceFactory.GetDocumentWithService
      └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
        └─ method WriteQuery [L63]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
      └─ uses_service IControlledRepository<DocumentTemplate> (Scoped (inferred))
        └─ method ReadQuery [L50]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentTemplateRepository.ReadQuery
  └─ sends_request CanIAuthorDocumentTemplatesQuery -> CanIAuthorDocumentTemplatesQueryHandler [L285]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
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
    └─ requests 2
      └─ CanIAuthorDocumentTemplatesQuery
      └─ GetDocumentTemplateDataQuery
    └─ handlers 2
      └─ CanIAuthorDocumentTemplatesQueryHandler
      └─ GetDocumentTemplateDataQueryHandler

