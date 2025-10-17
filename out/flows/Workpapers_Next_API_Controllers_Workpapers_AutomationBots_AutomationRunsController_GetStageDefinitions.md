[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stage-definitions  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStageDefinitions)  [L52–L58] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetStageDefinitionsForAutomationRunQuery -> GetStageDefinitionsForAutomationRunQueryHandler [L57]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStageDefinitionsForAutomationRunQueryHandler.Handle [L30–L53]
      └─ maps_to StageDefinitionDto [L51]
      └─ uses_service BotService
        └─ method GetStageDefinitions [L51]
          └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetStageDefinitions [L22-L111]
            └─ uses_service FirmFeatureFlagService
              └─ method GetBotDefinitions [L59]
                └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetBotDefinitions [L14-L110]
                  └─ calls FirmFeatureFlagRepository.ReadQuery [L107]
                  └─ uses_service FirmSettingsService
                    └─ method IsFirmPartOfControlledBeta [L94]
                      └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.IsFirmPartOfControlledBeta [L23-L154]
                        └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
                          └─ method GetSettings [L88]
                            └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.GetSettings
                        └─ uses_service DataverseService
                          └─ method GetSettings [L76]
                            └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetSettings [L17-L127]
                              └─ uses_service TenantIdentificationService
                                └─ method GetStandardQueryParams [L70]
                                  └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams [L15-L131]
                                    └─ uses_service RequestProcessor
                                      └─ method GetCatalogByFirmId [L104]
                                        └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ +1 additional_requests elided
                                    └─ uses_service FirmLightDto
                                      └─ method AssignActiveTenant [L77]
                                        └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
                        └─ uses_service TenantIdentificationService
                          └─ method GetSettings [L52]
                            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetSettings [L15-L131]
                              └─ uses_service RequestProcessor
                                └─ method GetCatalogByFirmId [L104]
                                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                  └─ +1 additional_requests elided
                              └─ uses_service FirmLightDto
                                └─ method AssignActiveTenant [L77]
                                  └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
                        └─ uses_service TenantService
                          └─ method GetCurrentSettings [L46]
                            └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings [L5-L22]
                              └─ uses_service TenantIdentificationService
                                └─ method GetCurrentTenant [L20]
                                  └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                                    └─ uses_service RequestProcessor
                                      └─ method GetCatalogByFirmId [L104]
                                        └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                        └─ +1 additional_requests elided
                                    └─ uses_service FirmLightDto
                                      └─ method AssignActiveTenant [L77]
                                        └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L60]
                  └─ uses_service FeatureFlagService
                    └─ method CanIUseFeature [L80]
                      └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FeatureFlagService.CanIUseFeature [L10-L34]
                        └─ calls FeatureFlagRepository.ReadQuery [L30]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L25]
                  └─ uses_service UserService
                    └─ method CanIUseFeature [L61]
                      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.CanIUseFeature [L20-L295]
                        └─ uses_service RequestProcessor
                          └─ method GetUserByDataverseId [L287]
                            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                            └─ +1 additional_requests elided
                        └─ uses_service bool?
                          └─ method IsSuperUser [L174]
                            └─ ... (no implementation details available)
                        └─ uses_service Firm>
                          └─ method GetFirmId [L139]
                            └─ ... (no implementation details available)
                        └─ uses_service User>
                          └─ method GetUserIdFromToken [L106]
                            └─ ... (no implementation details available)
                        └─ uses_service User
                          └─ method GetUserId [L67]
                            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
                        └─ uses_service Guid?
                          └─ method GetUserId [L64]
                            └─ ... (no implementation details available)
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L102]
            └─ uses_service Jurisdiction?
              └─ method GetBotDefinitions [L57]
                └─ ... (no implementation details available)
            └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
              └─ method GetBotDefinitions [L54]
                └─ implementation Workpapers.Next.Data.Repository.BinderRepository.GetBotDefinitions
            └─ uses_service RequestProcessor
              └─ method ExecuteStage [L43]
                └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ExecuteStage
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ExecuteStage
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ExecuteStage
                └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<AutomationRun> (Scoped (inferred))
        └─ method ReadQuery [L45]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.AutomationRunRepository.ReadQuery
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L55]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L71]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
            └─ logs ILogger<IRequestInfoService> [Warning] [L81]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ impact_summary
    └─ requests 2
      └─ CanIAccessBinderQuery
      └─ GetStageDefinitionsForAutomationRunQuery
    └─ handlers 2
      └─ CanIAccessBinderQueryHandler
      └─ GetStageDefinitionsForAutomationRunQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ StageDefinitionDto

