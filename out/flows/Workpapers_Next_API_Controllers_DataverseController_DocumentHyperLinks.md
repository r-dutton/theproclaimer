[web] PUT /api/dataverse/document-hyperlinks/{documentId:guid}  (Workpapers.Next.API.Controllers.DataverseController.DocumentHyperLinks)  [L410–L420] status=204 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ sends_request UpdateHyperlinksLinkedToDocumentCommand -> UpdateHyperlinksLinkedToDocumentCommandHandler [L417]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.UpdateHyperlinksLinkedToDocumentCommandHandler.Handle [L29–L118]
      └─ uses_service IControlledRepository<Matter> (Scoped (inferred))
        └─ method WriteQuery [L89]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterRepository.WriteQuery
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method WriteQuery [L75]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.WriteQuery
      └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
        └─ method WriteQuery [L61]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateHyperlinksLinkedToDocumentCommand
    └─ handlers 1
      └─ UpdateHyperlinksLinkedToDocumentCommandHandler

