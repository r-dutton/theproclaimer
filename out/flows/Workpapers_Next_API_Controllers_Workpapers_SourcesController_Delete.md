[web] DELETE /api/sources/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Delete)  [L163–L169] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteSourceCommand -> DeleteSourceCommandHandler [L166]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.Sources.DeleteSourceCommandHandler.Handle [L26–L58]
      └─ calls SourceRepository (methods: Remove,WriteQuery) [L43]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L50]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ DeleteSourceCommand
    └─ handlers 1
      └─ DeleteSourceCommandHandler

