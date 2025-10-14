[web] PUT /api/dataverse/document-hyperlinks/{documentId:guid}  (Workpapers.Next.API.Controllers.DataverseController.DocumentHyperLinks)  [L409–L419] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ sends_request UpdateHyperlinksLinkedToDocumentCommand [L416]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.UpdateHyperlinksLinkedToDocumentCommandHandler.Handle [L29–L118]
      └─ uses_service IControlledRepository<Matter>
        └─ method WriteQuery [L89]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L75]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method WriteQuery [L61]

