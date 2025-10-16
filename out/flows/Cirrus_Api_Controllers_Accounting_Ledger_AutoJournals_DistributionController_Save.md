[web] PUT /api/accounting/ledger/auto-journals/distributions/{datasetId:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.DistributionController.Save)  [L77–L103] status=200 [auth=user]
  └─ calls DatasetRepository.WriteQuery [L80]
  └─ calls DistributionRepository.Add [L96]
  └─ calls DistributionRepository.WriteQuery [L89]
  └─ insert Distribution [L96]
    └─ reads_from Distributions
  └─ write Distribution [L89]
    └─ reads_from Distributions
  └─ write Dataset [L80]
  └─ uses_service IControlledRepository<Dataset>
    └─ method WriteQuery [L80]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Distribution>
    └─ method WriteQuery [L89]
      └─ ... (no implementation details available)
  └─ sends_request MapSourceAccountsCommand [L87]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
      └─ calls SourceAccountsCachedRepository.AddTrackedRange [L92]
      └─ calls SourceAccountsCachedRepository.InDatabaseOnly [L80]
      └─ calls SourceAccountsCachedRepository.InMemoryOnly [L72]
      └─ uses_service IControlledRepository<Source>
        └─ method WriteQuery [L151]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L123]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L81]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]

