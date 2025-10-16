[web] POST /api/internal/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.Internal.Documents.DocumentVersionsController.AddDocumentVersion)  [L29–L34] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request AddDocumentVersionCommand -> AddDocumentVersionCommandHandler [L33]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AddDocumentVersionCommandHandler.Handle [L27–L94]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L88]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<DocumentStatus> (Scoped (inferred))
        └─ method WriteQuery [L78]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentStatusRepository.WriteQuery
      └─ uses_service RequestInfoService
        └─ method IsM2MRequest [L67]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
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
      └─ uses_service UserService
        └─ method GetUserId [L66]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
        └─ method ReadQuery [L61]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.ReadQuery
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method WriteQuery [L57]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ AddDocumentVersionCommand
    └─ handlers 1
      └─ AddDocumentVersionCommandHandler

