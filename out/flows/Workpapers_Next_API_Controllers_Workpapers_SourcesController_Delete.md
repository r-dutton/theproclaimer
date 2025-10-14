[web] DELETE /api/sources/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Delete)  [L163–L169] [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteSourceCommand [L166]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.Sources.DeleteSourceCommandHandler.Handle [L26–L58]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L50]
      └─ uses_service IControlledRepository<Source>
        └─ method WriteQuery [L41]

