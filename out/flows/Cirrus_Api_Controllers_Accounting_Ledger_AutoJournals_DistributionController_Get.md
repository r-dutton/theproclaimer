[web] GET /api/accounting/ledger/auto-journals/distributions/{datasetId:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.DistributionController.Get)  [L47–L59] [auth=user]
  └─ maps_to DistributionDto [L52]
    └─ automapper.registration CirrusMappingProfile (Distribution->DistributionDto) [L498]
  └─ calls DistributionRepository.ReadQuery [L52]
  └─ queries Distribution [L52]
    └─ reads_from Distributions
  └─ uses_service IControlledRepository<Distribution>
    └─ method ReadQuery [L52]
  └─ sends_request CanIAccessDatasetQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
      └─ uses_cache IDistributedCache [L116]
        └─ method SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache [L106]
        └─ method DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache [L103]
        └─ method CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache [L90]
        └─ method DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache [L88]
        └─ method CreateDatasetLockKey [write] [L88]

