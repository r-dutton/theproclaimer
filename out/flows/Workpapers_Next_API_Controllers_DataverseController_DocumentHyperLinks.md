[web] PUT /api/dataverse/document-hyperlinks/{documentId:guid}  (Workpapers.Next.API.Controllers.DataverseController.DocumentHyperLinks)  [L410–L420] status=204 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ sends_request UpdateHyperlinksLinkedToDocumentCommand [L417]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.UpdateHyperlinksLinkedToDocumentCommandHandler.Handle [L29–L118]
      └─ uses_service IControlledRepository<Matter>
        └─ method WriteQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Worksheet>
        └─ method WriteQuery [L61]
          └─ ... (no implementation details available)

