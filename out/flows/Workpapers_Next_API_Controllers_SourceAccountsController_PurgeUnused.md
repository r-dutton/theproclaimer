[web] DELETE /api/source-accounts/purge-unused  (Workpapers.Next.API.Controllers.SourceAccountsController.PurgeUnused)  [L315–L320] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request PurgeUnusedSourceAccountsCommand [L318]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.PurgeUnusedSourceAccountsCommandHandler.Handle [L36–L161]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L91]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L148]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Distribution>
        └─ method ReadQuery [L130]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L62]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

