[web] POST /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.CreateSubscription)  [L75–L116] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L109]
  └─ uses_client WorkpapersClient [L110]
    └─ calls CreateFirm (POST /api/dataverse/firms/) [L110]
      └─ remote_endpoint_lookup route=/api/dataverse/firms/ verb=POST
        └─ [web] POST /api/dataverse/firms  (Workpapers.Next.API.Controllers.DataverseController.CreateFirm)  [L234–L267] status=201 [auth=AuthorizationPolicies.M2M]
          └─ maps_to FirmLightWithSubscriptionsDto [L266]
          └─ maps_to FirmCreateModel (var firmModel) [L254]
            └─ automapper.registration DataverseMappingProfile (DataverseFirmCreateModel->FirmCreateModel) [L111]
          └─ uses_service UnitOfWork
            └─ method Table [L241]
              └─ implementation UnitOfWork.Table
  └─ uses_client WorkpapersClient [L100]
    └─ calls CreateFirm (POST /api/dataverse/firms/) [L110]
      └─ remote_endpoint_expansion_suppressed (see previous expansion)
    └─ calls Search [L60]
      └─ remote_endpoint_lookup route= verb=GET
        └─ [web] GET /api/ui/hnx/record-content/{binderId:guid}  (Dataverse.Api.Controllers.UI.HnxController.GetRecordContentForBinder)  [L30–L50] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/workpaper-records/search  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Search)  [L59–L88] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorkpaperRecordIndexDto [L79]
          └─ calls WorkpaperRecordRepository.ReadQuery [L64]
          └─ query WorkpaperRecord [L64]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L62]
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
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
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
              └─ uses_service UserService
                └─ method GetUserId [L91]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
                    └─ uses_service User
                      └─ method GetUserId [L67]
                        └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
                    └─ uses_service Guid?
                      └─ method GetUserId [L64]
                        └─ ... (no implementation details available)
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
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
        └─ [web] GET /api/internal/workpapers/binders/exist-for-client/{dataverseClientId:guid}  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.CheckForClientBinders)  [L47–L56] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ calls BinderRepository.ReadQuery [L50]
          └─ query Binder [L50]
            └─ reads_from Binders
        └─ [web] GET /api/companies-house/company/{companyNumber}/officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficers)  [L129–L139] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyOfficersQuery -> GetOfficersQueryHandler [L136]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficersQueryHandler.Handle [L27–L38]
        └─ [web] GET /api/entities/{id}  (Cirrus.Api.Controllers.Firm.EntitiesController.Get)  [L70–L88] status=200 [auth=user]
          └─ maps_to EntityDto [L85]
            └─ automapper.registration CirrusMappingProfile (Entity->EntityDto) [L165]
          └─ calls EntityRepository.ReadQuery [L75]
          └─ query Entity [L75]
          └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L84]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L83]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                    └─ ... (no dispatches detected)
              └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
                └─ method ReadQuery [L81]
                  └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L72]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L72]
                  └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
                    └─ uses_service IRequestProcessor (InstancePerDependency)
                      └─ method GetCatalogByDataverseId [L111]
                        └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                    └─ uses_service Tenant
                      └─ method AssignCurrentTenantId [L80]
                        └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
              └─ uses_service IRequestInfoService (AddScoped)
                └─ method IsValidServiceAccountRequest [L67]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L92]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L74]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L72]
          └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L80]
        └─ [web] GET /api/workpaper-records/external-values/evaluate  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.EvaluateExternalValue)  [L244–L248] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request EvaluateExternalValueQuery -> EvaluateExternalValueQueryHandler [L247]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.ExternalValues.EvaluateExternalValueQueryHandler.Handle [L54–L126]
              └─ uses_service ICirrusQueryService (AddScoped)
                └─ method GetDataset [L121]
                  └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDataset [L18-L250]
                    └─ uses_service CirrusHttp
                      └─ method GetAccounts [L33]
                        └─ ... (no implementation details available)
                    └─ uses_service CirrusConfig
                      └─ method GetAccounts [L31]
                        └─ ... (no implementation details available)
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L110]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L104]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
                └─ method ReadQuery [L89]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
        └─ [web] GET /api/accounting/ledger/journals/for-dataset/{datasetId}/report  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetJournalReport)  [L109–L113] status=200 [auth=user]
        └─ [web] GET /api/workpaper-records/  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Get)  [L102–L121] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorkpaperRecordIndexDto [L106]
            └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordIndexDto) [L523]
          └─ calls WorkpaperRecordRepository.ReadQuery [L106]
          └─ query WorkpaperRecord [L106]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L116]
        └─ [web] GET /api/accounting/reports/published/file/{publishedReportFileId:guid}/download  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.DownloadLink)  [L128–L157] status=200 [auth=user]
          └─ calls PublishedReportBatchRepository.ReadQuery [L147]
          └─ calls PublishedReportFileRepository.ReadQuery [L132]
          └─ query PublishedReportBatch [L147]
            └─ reads_from PublishedReportBatches
          └─ query PublishedReportFile [L132]
            └─ reads_from PublishedReportFiles
          └─ uses_service IRequestProcessor (InstancePerDependency)
            └─ method ProcessAsync [L133]
              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L133]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L90]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L68]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service ITenantService (AddScoped)
                └─ method GetCurrentTenant [L68]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
                    └─ uses_service TenantIdentificationService
                      └─ method GetCurrentTenant [L20]
                        └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                          └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                          └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
              └─ uses_service IRequestInfoService (AddScoped)
                └─ method IsValidServiceAccountRequest [L66]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L152]
            └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
              └─ uses_service IStorageService (InstancePerLifetimeScope)
                └─ method CreateDownloadUrl [L60]
                  └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
                    └─ uses_service RequestContextProvider
                      └─ method GetContainer [L89]
                        └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
              └─ uses_storage IStorageService.CreateDownloadUrl [L60]
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L140]
        └─ [web] GET /api/accounting/ledger/cashflow/journals/package  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetPackage)  [L71–L75] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetCashflowJournalPackageQuery -> GetCashflowJournalPackageQueryHandler [L74]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowJournalPackageQueryHandler.Handle [L46–L118]
              └─ uses_service IControlledRepository<CashflowJournal> (Scoped (inferred))
                └─ method ReadQuery [L99]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.Cashflow.CashflowJournalRepository.ReadQuery
              └─ uses_service GetCashflowSimpleLinesQuery
                └─ method Execute [L92]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowReconciliationLinesQuery
                └─ method Execute [L87]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowLinesQuery
                └─ method Execute [L82]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L69]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
        └─ [web] GET /api/hmrc  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetBinder)  [L98–L103] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L100]
          └─ query Binder [L100]
            └─ reads_from Binders
        └─ [web] GET /api/connection/{apiType}/token  (DataGet.Api.Controllers.Connections.ConnectionController.GetToken)  [L170–L179] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/internal/clients/audit  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetAll)  [L42–L48] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_client ClientRepository [L45]
        └─ [web] GET /api/internal/account-mapping/mapping-codes  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodes)  [L59–L68] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetMappingCodesQuery -> GetMappingCodesQueryHandler [L67]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodesQueryHandler.Handle [L36–L107]
              └─ maps_to MappingCodeDto [L89]
              └─ uses_service IControlledRepository<ExcludedMappingCode> (Scoped (inferred))
                └─ method ReadQuery [L65]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExcludedMappingCodeRepository.ReadQuery
              └─ uses_service UserService
                └─ method IsMaster [L58]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
                    └─ calls UserRepository.ReadQuery [L133]
                    └─ uses_service RequestInfoService
                      └─ method GetIdentityId [L160]
                        └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                          └─ uses_service RequestInfo
                            └─ method IsValidServiceAccountRequest [L82]
                              └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                              └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                              └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                          └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                    └─ uses_service User
                      └─ method GetUserId [L43]
                        └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                        └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
                    └─ uses_service Guid?
                      └─ method GetUserId [L40]
                        └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<MappingCode> (Scoped (inferred))
                └─ method ReadQuery [L53]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.MappingCodeRepository.ReadQuery
        └─ [web] GET /api/accounting/reports/page-layouts/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.Get)  [L45–L49] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetReportPageLayoutQuery -> GetReportPageLayoutsQueryHandler [L48]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportPageLayoutsQueryHandler.Handle [L47–L197]
              └─ maps_to ReportPageLayoutDto [L124]
              └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
                └─ method ReadQuery [L95]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L70]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/users/checkemail  (Workpapers.Next.API.Controllers.UsersController.CheckEmail)  [L221–L231] status=200
          └─ calls UserRepository.ReadQuery [L228]
          └─ query User [L228]
          └─ uses_service UserRepository
            └─ method ReadQuery [L228]
              └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
        └─ [web] GET /health/api  (Workpapers.Next.API.Controllers.HealthController.Health)  [L16–L20] status=200 [AllowAnonymous]
        └─ [web] GET /api/internal/data-cloud/tenants  (Dataverse.Api.Controllers.Internal.DataCloudController.GetTenants)  [L55–L59] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/accounting/reports/pageTypes/{id}/light  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetLight)  [L72–L78] status=200 [auth=user]
          └─ maps_to ReportPageTypeLightWithContentFieldsDto [L75]
            └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightWithContentFieldsDto) [L635]
          └─ calls ReportPageTypeRepository.ReadQuery [L75]
          └─ query ReportPageType [L75]
            └─ reads_from ReportPageTypes
        └─ [web] GET /api/super/users/  (Dataverse.Api.Controllers.Super.Core.UsersController.SearchByTenant)  [L62–L76] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request FindUsersWithLicensesQuery [L74]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValue)  [L39–L50] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to KeyValueDto [L44]
          └─ calls WorksheetRepository.ReadQuery [L44]
          └─ query Worksheet [L44]
            └─ reads_from Worksheets
          └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L42]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L58]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L54]
                  └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
        └─ [web] GET /api/super/documents/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Documents.DocumentSubscriptionController.GetSubscription)  [L42–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L47]
          └─ calls TenantRepository.ReadTable [L47]
          └─ query Tenant [L47]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L47]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
        └─ [web] GET /api/internal/task-templates  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Get)  [L33–L43] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TaskTemplateDto [L36]
            └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
          └─ calls TaskTemplateRepository.ReadQuery [L36]
          └─ query TaskTemplate [L36]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/template-help-contents/for-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.GetAllHelpContent)  [L50–L58] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to TemplateHelpContentDto [L53]
            └─ automapper.registration WorkpapersMappingProfile (TemplateHelpContent->TemplateHelpContentDto) [L326]
          └─ calls TemplateHelpContentRepository.ReadQuery [L53]
          └─ query TemplateHelpContent [L53]
            └─ reads_from TemplateHelpContents
        └─ [web] GET /api/connections/myob/es/files  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetAccountingFiles)  [L26–L32] status=200
          └─ sends_request GetBusinessesQuery -> GetBusinessesQueryHandler [L29]
            └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessesQueryHandler.Handle [L10–L29]
              └─ uses_service IMapToNew<Business, AccountingFileDto>
                └─ method Map [L27]
                  └─ ... (no implementation details available)
              └─ uses_service MyobEssentialsHttp
                └─ method GetHttpResponseAsync [L24]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/templates/test  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetTestReport)  [L150–L154] status=200 [auth=user]
          └─ sends_request GetTestReportQuery -> GetTestReportQueryHandler [L153]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetTestReportQueryHandler.Handle [L28–L64]
              └─ maps_to ReportSettingsDto [L58]
                └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
              └─ uses_service IControlledRepository<ReportSettings> (Scoped (inferred))
                └─ method ReadQuery [L58]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportSettingsRepository.ReadQuery
              └─ uses_service IControlledRepository<Office> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
              └─ uses_service FileManagerService
                └─ method GetCurrentFolder [L45]
                  └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]
        └─ [web] GET /api/ui/workflow/jobs/{id:guid}/cirrus-links  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobCirrusLinks)  [L83–L93] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DeliverableCirrusLinkDto [L86]
          └─ calls JobRepository.ReadQuery [L86]
          └─ query Job [L86]
            └─ reads_from Jobs
        └─ [web] GET /api/accounting/assets/reports/pool/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetDetailReportExcel)  [L171–L177] status=200 [auth=user]
          └─ sends_request GetExportAssetReportQuery -> GetExportAssetReportQueryHandler [L175]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetExportAssetReportQueryHandler.Handle [L69–L147]
              └─ uses_service IControlledRepository<DepreciationYear> (Scoped (inferred))
                └─ method ReadQuery [L100]
                  └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationYearRepository.ReadQuery
              └─ uses_service IControlledRepository<ReportPageType> (Scoped (inferred))
                └─ method ReadQuery [L99]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportPageTypeRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L89]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/accounting-file/{fileId}/divisions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetSourceDivisions)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDivisionsQuery -> GetDivisionsQueryHandler [L38]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetDivisionsQueryHandler.Handle [L22–L54]
              └─ maps_to SourceDivisionDto [L49]
              └─ uses_client DataGetClient [L45]
              └─ uses_service DataGetClient
                └─ method GetDivisionsAsync [L45]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDivisionsAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service IControlledRepository<SourceDivision> (Scoped (inferred))
                └─ method ReadQuery [L41]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceDivisionRepository.ReadQuery
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L39]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
                    └─ uses_service IControlledRepository<Source> (Scoped (inferred))
                      └─ method GetFileIdFromSource [L25]
                        └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
        └─ [web] GET /api/fyi-elite/access-info  (DataGet.Api.Controllers.Integrations.FyiEliteController.GetAccessInfo)  [L64–L77] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service UserTokenService
            └─ method GetTokenAsync [L69]
              └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
                └─ uses_service ConnectionContextProvider
                  └─ method GetTokenAsync [L28]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/fyi/documents/external  (DataGet.Api.Controllers.Integrations.FyiController.GetExternalDocument)  [L122–L131] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetExternalDocumentQuery -> GetExternalDocumentQueryHandler [L127]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetExternalDocumentQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetExternalDocument [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetExternalDocument [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
        └─ [web] GET /api/super/teams/  (Dataverse.Api.Controllers.Super.Core.TeamsController.GetAll)  [L30–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TeamLightDto [L33]
            └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L150]
          └─ calls TeamRepository.ReadQuery [L33]
          └─ query Team [L33]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L33]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
        └─ [web] GET /api/workpaperitems/{id:guid}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Get)  [L58–L70] status=200
          └─ maps_to WorkpaperItemWithConversationDto [L61]
          └─ uses_service UnitOfWork
            └─ method Table [L61]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/ledger/journals/for-worksheet/{linkedWorksheetId:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetForWorksheet)  [L139–L150] status=200 [auth=user]
          └─ maps_to JournalDto [L142]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L148]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L129]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L127]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L103]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IRequestInfoService (AddScoped)
                └─ method IsValidServiceAccountRequest [L101]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_service ITenantService (AddScoped)
                └─ method GetCurrentTenant [L88]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
              └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]
        └─ [web] GET /api/binders/{binderId:guid}/automation-runs  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetAll)  [L44–L50] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetAutomationRunsQuery -> GetAutomationRunsQueryHandler [L49]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationRunsQueryHandler.Handle [L35–L93]
              └─ maps_to AutomationRunStageDto [L67]
                └─ converts_to AutomationRunStageModel [L961]
              └─ maps_to AutomationRunLightDto [L56]
                └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunLightDto) [L914]
              └─ uses_service BotService
                └─ method GetStageDefinitions [L82]
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
                                                └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
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
                                          └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant (see previous expansion)
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
                                    └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
                                └─ uses_service Guid?
                                  └─ method GetUserId [L64]
                                    └─ ... (no implementation details available)
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
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service IControlledRepository<AutomationRun> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.AutomationRunRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L47]
        └─ [web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetCreateBinderParams)  [L541–L566] status=200
          └─ calls BinderTemplateRepository.ReadQuery [L543]
          └─ query BinderTemplate [L543]
            └─ reads_from BinderTemplates
          └─ uses_service UserService
            └─ method GetUser [L565]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
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
                    └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
                └─ uses_service Guid?
                  └─ method GetUserId [L64]
                    └─ ... (no implementation details available)
          └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
            └─ method WriteQuery [L556]
              └─ ... (no implementation details available)
        └─ [web] GET /workpapers/v1/workpaper-records/{recordId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.Get)  [L44–L50] status=200
          └─ maps_to WorkpaperRecordDto [L47]
            └─ automapper.registration ExternalApiMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L169]
            └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L562]
          └─ calls WorkpaperRecordRepository.ReadQuery [L47]
          └─ query WorkpaperRecord [L47]
            └─ reads_from WorkpaperRecords
        └─ [web] GET /api/accounting-file/{fileId}/wages  (DataGet.Api.Controllers.Connections.AccountingFileController.GetWages)  [L129–L145] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetWagesQuery -> GetWagesQueryHandler [L134]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetWagesQueryHandler.Handle [L26–L45]
              └─ uses_client DataGetClient [L41]
                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                  └─ target_service DataGet.Api
                    └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetWages
              └─ uses_service DataGetClient
                └─ method GetWagesAsync [L41]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetWagesAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L39]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
        └─ [web] GET /api/binder-sections/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderSectionsController.Get)  [L41–L51] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderSectionDto [L44]
            └─ automapper.registration WorkpapersMappingProfile (BinderSection->BinderSectionDto) [L465]
            └─ automapper.registration ExternalApiMappingProfile (BinderSection->BinderSectionDto) [L189]
          └─ calls BinderSectionRepository.ReadQuery [L44]
          └─ query BinderSection [L44]
            └─ reads_from BinderSections
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L48]
        └─ [web] GET /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.GetAll)  [L41–L49] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to FirmDto [L44]
            └─ converts_to FirmWithStatsDto [L162]
          └─ calls CentralRepository.ReadTable [L44]
          └─ uses_service CentralRepository
            └─ method ReadTable [L44]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
        └─ [web] GET /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Get)  [L72–L76] status=200
          └─ sends_request GetWorkpaperRecordTemplateQuery -> GetWorkpaperRecordTemplateQueryHandler [L75]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateQueryHandler.Handle [L22–L144]
              └─ maps_to SectionLightDto [L82]
              └─ maps_to WorkpaperRecordTemplateLightWithTemplatesDto [L73]
              └─ maps_to WorkpaperRecordTemplateUltraLightDto [L56]
              └─ maps_to WorkpaperRecordTemplateAsFirmDto [L49]
              └─ maps_to WorkpaperRecordTemplateAsSuperDto [L48]
              └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery
              └─ uses_service UnitOfWork
                └─ method Table [L51]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service UserService
                └─ method IsSuperUser [L47]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
                    └─ uses_service bool?
                      └─ method IsSuperUser [L174]
                        └─ ... (no implementation details available)
        └─ [web] GET /api/imanage/customers/{customerId:int}/folders  (DataGet.Api.Controllers.Integrations.IManageController.GetFolders)  [L228–L235] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageFolderQuery -> GetIManageFolderQueryHandler [L234]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageFolderQueryHandler.Handle [L19–L32]
              └─ uses_service IManageService
                └─ method GetFolders [L28]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetFolders [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation [L17-L95]
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/journals/package  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetPackage)  [L186–L191] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetJournalPackageQuery -> GetJournalPackageQueryHandler [L190]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalPackageQueryHandler.Handle [L56–L244]
              └─ maps_to DivisionLightDto [L166]
                └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
              └─ maps_to StandardAccountLightListForJournalDto [L155]
                └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightListForJournalDto) [L447]
              └─ maps_to CachedSourceAccountDto [L134]
                └─ automapper.registration CirrusMappingProfile (CachedSourceAccount->CachedSourceAccountDto) [L287]
              └─ maps_to SourceAccountLightDto [L124]
                └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
                └─ converts_to LinkedSourceAccount [L72]
                └─ converts_to SourceAccountInJournalModel [L269]
                └─ converts_to MappingSourceAccountModel [L830]
              └─ maps_to AccountLightListForJournalDto [L115]
                └─ automapper.registration CirrusMappingProfile (Account->AccountLightListForJournalDto) [L318]
              └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
                └─ method ReadQuery [L177]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.ReadQuery
              └─ uses_service IControlledRepository<Division> (Scoped (inferred))
                └─ method ReadQuery [L166]
                  └─ implementation Cirrus.Data.Repository.Accounting.Setup.DivisionRepository.ReadQuery
              └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
                └─ method ReadQuery [L155]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L134]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L124]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L115]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L99]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L95]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ returns JournalPackageDto [L190] [return]
        └─ [web] GET /api/teams/{id:Guid}  (Workpapers.Next.API.Controllers.TeamController.GetTeam)  [L70–L80] status=200
          └─ maps_to TeamDto [L79]
          └─ uses_service UnitOfWork
            └─ method Table [L73]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/reports/styles/css/whitelist  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetWhiteListCssProperties)  [L73–L79] status=200 [auth=Authentication.AdminPolicy]
          └─ sends_request GetWhiteListCssPropertiesQuery -> GetWhiteListCssPropertiesQueryHandler [L77]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetWhiteListCssPropertiesQueryHandler.Handle [L12–L19]
        └─ [web] GET /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Get)  [L38–L44] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to FirmSettingsDto [L41]
            └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
          └─ calls FirmSettingsRepository.ReadQuery [L41]
          └─ query FirmSettings [L41]
            └─ reads_from FirmSettingss
        └─ [web] GET /api/binder-fields/for-binder-type/{binderTypeId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAllByProduct)  [L36–L48] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderFieldDto [L40]
            └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
          └─ calls BinderFieldRepository.ReadQuery [L40]
          └─ query BinderField [L40]
            └─ reads_from BinderFields
        └─ [web] GET /api/internal/contacts/audit  (Dataverse.Api.Controllers.Internal.Core.ContactsController.GetAll)  [L40–L44] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls ContactRepository.ReadQuery [L43]
          └─ query Contact [L43]
            └─ reads_from DVS_Clients
        └─ [web] GET /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.GetAll)  [L39–L49] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to PolicyLayoutLightDto [L44]
            └─ automapper.registration CirrusMappingProfile (PolicyLayout->PolicyLayoutLightDto) [L782]
          └─ calls PolicyLayoutRepository.ReadQuery [L44]
          └─ query PolicyLayout [L44]
            └─ reads_from PolicyLayouts
          └─ uses_service IJurisdictionService (AddScoped)
            └─ method GetFirmJurisdictions [L42]
              └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetFirmJurisdictions [L7-L27]
                └─ uses_service FirmSettingsService
                  └─ method GetFirmJurisdictions [L18]
                    └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetFirmJurisdictions [L23-L154]
                      └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
                        └─ method GetSettings [L88]
                          └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.GetSettings
                      └─ uses_service DataverseService
                        └─ method GetSettings [L76]
                          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetSettings (see previous expansion)
                      └─ uses_service TenantIdentificationService
                        └─ method GetSettings [L52]
                          └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetSettings (see previous expansion)
                      └─ uses_service TenantService
                        └─ method GetCurrentSettings [L46]
                          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings (see previous expansion)
        └─ [web] GET /api/binders/{binderId:guid}/documents/  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentsController.FindDocuments)  [L23–L28] status=200 [auth=AuthorizationPolicies.User]
        └─ [web] GET /api/clients/summary  (Workpapers.Next.API.Controllers.ClientsController.GetSummary)  [L125–L170] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_client ClientRepository [L150]
          └─ uses_client ClientRepository [L128]
          └─ calls BinderRepository.ReadQuery [L140]
          └─ query Binder [L140]
            └─ reads_from Binders
        └─ [web] GET /api/internal/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L35] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetBenchmarkCodeSetQuery -> GetBenchmarkCodeSetQueryHandler [L34]
            └─ handled_by Cirrus.Central.Dataverse.Queries.AccountMapping.GetBenchmarkCodeSetQueryHandler.Handle [L27–L62]
              └─ uses_service DataverseService
                └─ method Get [L50]
                  └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
                    └─ uses_service TenantService
                      └─ method GetStandardQueryParams [L62]
                        └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                          └─ uses_service IRequestProcessor (InstancePerDependency)
                            └─ method GetCatalogByDataverseId [L111]
                              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
                          └─ uses_service Tenant
                            └─ method AssignCurrentTenantId [L80]
                              └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L49]
                  └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L42]
                  └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
                    └─ uses_service IRequestInfoService (AddScoped)
                      └─ method GetIdentityId [L103]
                        └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
                    └─ uses_service IControlledRepository<User> (Scoped (inferred))
                      └─ method GetUserId [L50]
                        └─ implementation Cirrus.Data.Repository.Firm.UserRepository.GetUserId
                    └─ uses_service User
                      └─ method GetUserId [L37]
                        └─ implementation Cirrus.DomainModel.Model.Firm.User.GetUserId [L18-L345]
                    └─ uses_service Guid?
                      └─ method GetUserId [L34]
                        └─ ... (no implementation details available)
          └─ returns DataverseBenchmarkCodeSetDto [L34] [return]
        └─ [web] GET /api/licensing  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackage)  [L49–L95] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to FirmWithSubscriptionsDto (var Firm) [L86]
            └─ converts_to FirmWithStatsDto [L170]
          └─ maps_to UserDto (var User) [L85]
          └─ uses_service UserService
            └─ method GetUserId [L53]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L52]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/charges/{chargeId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharge)  [L171–L179] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyChargeQuery -> GetCompanyChargeQueryHandler [L176]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargeQueryHandler.Handle [L16–L26]
        └─ [web] GET /api/binders/client-queries  (Workpapers.Next.API.Controllers.Workpapers.BindersController.SearchClientQuery)  [L116–L121] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request FindClientQueriesForBindersQuery -> FindClientQueryHandler [L120]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindClientQueryHandler.Handle [L40–L119]
              └─ calls ClientRepository.ReadQuery [L78]
              └─ uses_client ClientRepository [L70]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L85]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
                    └─ uses_service TenantService
                      └─ method GetCurrentSettings [L46]
                        └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L85]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L84]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
        └─ [web] GET /api/companies-house/company/{companyNumber}/filing-history  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistory)  [L181–L191] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyFilingHistoryQuery -> GetCompanyFilingHistoryQueryHandler [L188]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryQueryHandler.Handle [L23–L35]
        └─ [web] GET /api/ui/sync-configuration/{id:guid}/authorisation-url  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAuthorisationUrl)  [L191–L195] status=200 [auth=Authentication.AdminPolicy]
        └─ [web] GET /workpapers/v1/matters/{matterId:Guid}/messages  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMatterWithMessages)  [L57–L63] status=200
          └─ maps_to MatterWithMessagesDto [L60]
            └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
            └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
          └─ calls MatterRepository.ReadQuery [L60]
          └─ query Matter [L60]
            └─ reads_from Matters
        └─ [web] GET /api/gov-link/activity-statements/  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatements)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetActivityStatementPackage [L27]
              └─ implementation GovLink.Application.Services.AtoService.GetActivityStatementPackage [L12-L145]
        └─ [web] GET /api/dataverse/firms/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.GetFirm)  [L199–L209] status=200 [auth=AuthorizationPolicies.M2M]
          └─ maps_to FirmLightWithSubscriptionsDto [L203]
          └─ uses_service UnitOfWork
            └─ method Table [L203]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/dataverse/{entityRoute}/audit-entity/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.GetAudit)  [L385–L395] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ sends_request DataverseEntityAuditQuery -> DataverseEntityAuditQueryHandler [L392]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseEntityAuditQueryHandler.Handle [L24–L57]
              └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
                └─ method ReadQuery [L52]
                  └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
              └─ uses_service IControlledRepository<Client> (Scoped (inferred))
                └─ method ReadQuery [L50]
                  └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
              └─ uses_service IControlledRepository<User> (Scoped (inferred))
                └─ method ReadQuery [L48]
                  └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
              └─ uses_service IControlledRepository<Office> (Scoped (inferred))
                └─ method ReadQuery [L46]
                  └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
          └─ returns DataverseAuditDto [L392]
        └─ [web] GET /api/connections/{apiType}  (Workpapers.Next.API.Controllers.Connections.ConnectionsController.GetConnections)  [L44–L52] status=200
          └─ uses_service UserService
            └─ method GetFirmId [L49]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
                └─ uses_service Firm>
                  └─ method GetFirmId [L139]
                    └─ ... (no implementation details available)
          └─ uses_service UnitOfWork
            └─ method Table [L48]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/super/cirrus/storage-accounts  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetAllStorageAccounts)  [L128–L132] status=200 [auth=Authentication.MasterPolicy]
          └─ uses_client CirrusClient [L131]
            └─ calls GetAll (GET /api/central/storage-accounts) [L131]
              └─ remote_endpoint_lookup route=/api/central/storage-accounts verb=GET
                └─ [web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
                  └─ maps_to StorageAccountDto [L31]
                  └─ calls CentralRepository.ReadTable [L31]
                  └─ uses_service CentralRepository
                    └─ method ReadTable [L31]
                      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Get)  [L34–L39] status=200 [auth=user]
          └─ maps_to FooterTemplateDto [L37]
            └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
            └─ converts_to FooterContentDto [L49]
          └─ calls FooterTemplateRepository.ReadQuery [L37]
          └─ query FooterTemplate [L37]
            └─ reads_from ReportFooterTemplates
        └─ [web] GET /api/super/tenants/license-summary  (Dataverse.Api.Controllers.Super.TenantController.GetLicenseSummary)  [L122–L151] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to LicenseSummaryDto [L126]
          └─ calls UserRepository.ReadQuery [L133]
          └─ calls TenantRepository.ReadTable [L126]
          └─ query User [L133]
          └─ query Tenant [L126]
            └─ reads_from Tenants
          └─ uses_service UserRepository
            └─ method ReadQuery [L133]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
          └─ uses_service TenantRepository
            └─ method ReadTable [L126]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/ui/fyi  (Dataverse.Api.Controllers.UI.FyiController.UploadFile)  [L302–L307] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method UploadFile [L304]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.UploadFile [L19-L291]
        └─ [web] GET /api/central/firms/tenants  (Cirrus.Api.Controllers.Central.FirmController.FindTenants)  [L51–L61] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntity)  [L274–L282] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscCorporateEntityQuery -> GetCompanyPscCorporateEntityQueryHandler [L279]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityQueryHandler.Handle [L19–L31]
        └─ [web] GET /api/accounting/ledger/reports/{datasetId}/trialbalance  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetTrialBalance)  [L47–L70] status=200 [auth=user]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L52]
        └─ [web] GET /api/accounting/ledger/standard-charts  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetCombinedMasterStandardQuery)  [L180–L204] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardChartDto [L188]
            └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
            └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
          └─ calls StandardChartRepository.ReadQuery [L188]
          └─ query StandardChart [L188]
            └─ reads_from StandardCharts
        └─ [web] GET /workpapers/v1/sources/{sourceId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.Get)  [L43–L49] status=200
          └─ maps_to SourceDto [L46]
            └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
            └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
          └─ calls SourceRepository.ReadQuery [L46]
          └─ query Source [L46]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L46]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
        └─ [web] GET /api/ui/tenants/license-summary  (Dataverse.Api.Controllers.UI.TenantController.GetLicenseSummary)  [L70–L95] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to LicenseSummaryDto [L74]
          └─ calls UserRepository.ReadQuery [L81]
          └─ calls TenantRepository.ReadTable [L74]
          └─ query User [L81]
          └─ query Tenant [L74]
            └─ reads_from Tenants
          └─ uses_service UserRepository
            └─ method ReadQuery [L81]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
          └─ uses_service TenantRepository
            └─ method ReadTable [L74]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L73]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
                └─ uses_service TenantIdentificationService
                  └─ method GetCurrentTenant [L20]
                    └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
        └─ [web] GET /api/ui/visualisations/heatmap/find/client  (Dataverse.Api.Controllers.UI.Visualisations.HeatMapController.FindClientHeatMap)  [L40–L44] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/imanage/api-information  (DataGet.Api.Controllers.Integrations.IManageController.TestConnection)  [L138–L146] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageApiInformationQuery -> GetIManageApiInformationQueryHandler [L142]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
              └─ uses_service IManageService
                └─ method GetApiInformation [L23]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetApiInformation [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
        └─ [web] GET /workpapers/v1/source-accounts/{sourceAccountId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.Get)  [L43–L49] status=200
          └─ maps_to SourceAccountDto [L46]
            └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountDto) [L625]
            └─ automapper.registration ExternalApiMappingProfile (SourceAccount->SourceAccountDto) [L105]
          └─ calls SourceAccountRepository.ReadQuery [L46]
          └─ query SourceAccount [L46]
            └─ reads_from SourceAccounts
          └─ uses_service SourceAccountRepository
            └─ method ReadQuery [L46]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery [L8-L38]
        └─ [web] GET /api/source-accounts/for-source/{sourceId}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForSource)  [L71–L83] status=200 [auth=AuthorizationPolicies.User]
          └─ calls SourceRepository.ReadQuery [L74]
          └─ query Source [L74]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L74]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
          └─ sends_request GetSourceAccountsQuery -> GetSourceAccountsQueryHandler [L82]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsQueryHandler.Handle [L50–L99]
              └─ calls SourceAccountRepository.ReadQuery [L78]
              └─ uses_service SourceAccountsQueryProcessor
                └─ method ProcessSourceAccounts [L95]
                  └─ ... (no implementation details available)
          └─ sends_request GetSourceAccountsLightQuery [L81]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/for-binder  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetForBinder)  [L107–L114] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetWorkSheetsForBinderQuery -> GetWorkSheetsForBinderQueryHandler [L111]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.GetWorkSheetsForBinderQueryHandler.Handle [L35–L98]
              └─ maps_to WorksheetDto [L65]
              └─ uses_service MatterCountQueryProcessor
                └─ method ProcessMatterCounts [L90]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L110]
        └─ [web] GET /api/fyi  (DataGet.Api.Controllers.Integrations.FyiController.SetConnectionContext)  [L319–L322] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/accounting/reports/notes/reporting-suites/unselected-disclosure-variants  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetUnselectedDisclosureVariants)  [L92–L100] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDisclosureTemplatesWithVariantsQuery [L98]
        └─ [web] GET /api/internal/tenants/{id}  (Dataverse.Api.Controllers.Internal.TenantController.Get)  [L53–L60] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to TenantDto [L56]
          └─ calls TenantRepository.ReadTable [L56]
          └─ query Tenant [L56]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L56]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /workflow/v1/kanban-columns/  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.MinimalQuery)  [L65–L71] status=200
          └─ calls KanbanColumnRepository.ReadQuery [L69]
          └─ query KanbanColumn [L69]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetAll)  [L159–L179] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to OfficeLightDto [L173]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
          └─ calls OfficeRepository.ReadQuery [L173]
          └─ query Office [L173]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L173]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L167]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
                └─ uses_client WorkpapersClient [L78]
                └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
                  └─ method GetCurrentSettingsAsync [L52]
                    └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
                └─ uses_service TenantService
                  └─ method GetCurrentSettingsAsync [L44]
                    └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
                      └─ uses_service TenantIdentificationService
                        └─ method GetCurrentTenant [L20]
                          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
                └─ maps_to FirmSettingsDto [L60]
                └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
                └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
          └─ uses_service UserService
            └─ method IsInRole [L162]
              └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]
                └─ calls UserRepository.ReadQuery [L133]
                └─ uses_service RequestInfoService
                  └─ method GetIdentityId [L160]
                    └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
                └─ uses_service User
                  └─ method GetUserId [L43]
                    └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                    └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
                └─ uses_service Guid?
                  └─ method GetUserId [L40]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/published/batch/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Detail)  [L94–L100] status=200 [auth=user]
          └─ maps_to PublishedReportBatchDto [L99]
          └─ calls PublishedReportBatchRepository.ReadQuery [L97]
          └─ query PublishedReportBatch [L97]
            └─ reads_from PublishedReportBatches
          └─ uses_service IRequestProcessor (InstancePerDependency)
            └─ method ProcessAsync [L98]
              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L98]
        └─ [web] GET /api/ui/imanage/standard-user-authorisation-url  (Dataverse.Api.Controllers.UI.IManageController.GetAuthorisationUrl)  [L70–L77] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetAuthorisationUrlForStandardUser [L75]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetAuthorisationUrlForStandardUser [L19-L225]
        └─ [web] GET /api/ui/workflow/tasks/for-job/{jobId:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetJobTasks)  [L59–L72] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to WorkflowTaskLightDto [L65]
          └─ calls TaskRepository.ReadQuery [L65]
          └─ uses_service TaskRepository
            └─ method ReadQuery [L65]
              └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery [L8-L40]
          └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L63]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIAccessJobQueryHandler.Handle [L39–L95]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L82]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Job> (Scoped (inferred))
                └─ method ReadQuery [L78]
                  └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L73]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserAsync [L71]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
                    └─ calls UserRepository.ReadQuery [L133]
                    └─ uses_service RequestInfoService
                      └─ method GetIdentityId [L160]
                        └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
                    └─ uses_service User
                      └─ method GetUserId [L43]
                        └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                        └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
                    └─ uses_service Guid?
                      └─ method GetUserId [L40]
                        └─ ... (no implementation details available)
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L68]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L75]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L73]
        └─ [web] GET /api/ui/fyi/documents/{documentId:guid}  (Dataverse.Api.Controllers.UI.FyiController.GetDocument)  [L98–L104] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetDocumentAsync [L101]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentAsync [L19-L291]
        └─ [web] GET /api/fyi-elite  (DataGet.Api.Controllers.Integrations.FyiEliteController.SetConnectionContext)  [L103–L106] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Get)  [L130–L138] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to MatterTextTemplateDto [L134]
            └─ automapper.registration WorkpapersMappingProfile (MatterTextTemplate->MatterTextTemplateDto) [L800]
          └─ calls MatterTextTemplateRepository.ReadQuery [L134]
          └─ query MatterTextTemplate [L134]
            └─ reads_from MatterTextTemplates
        └─ [web] GET /api/ui/imanage/access-info  (Dataverse.Api.Controllers.UI.IManageController.GetAccessInfo)  [L79–L97] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetAccessInfo [L84]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetAccessInfo [L19-L225]
        └─ [web] GET /api/companies-house/officers/{officerId}/appointments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetOfficerAppointmentList)  [L243–L252] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetOfficerAppointmentsQuery -> GetOfficerAppointmentsQueryHandler [L249]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerAppointmentsQueryHandler.Handle [L23–L38]
        └─ [web] GET /api/accounting/reports/styles/css/editable-defaults  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetEditableStyleDefaultsCss)  [L61–L71] status=200 [auth=Authentication.AdminPolicy]
          └─ sends_request GetDefaultPdfCssQuery -> GetPdfCssQueryHandler [L65]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfCssQueryHandler.Handle [L13–L38]
              └─ uses_service FileManagerService
                └─ method GetCurrentFolder [L23]
                  └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder (see previous expansion)
        └─ [web] GET /api/template-standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccount)  [L45–L53] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to TemplateStandardAccountDto [L49]
            └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccount->TemplateStandardAccountDto) [L674]
          └─ calls TemplateStandardAccountRepository.ReadQuery [L49]
          └─ query TemplateStandardAccount [L49]
            └─ reads_from TemplateStandardAccounts
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPerson)  [L334–L342] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscLegalPersonQuery -> GetCompanyPscLegalPersonQueryHandler [L339]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonQueryHandler.Handle [L19–L31]
        └─ [web] GET /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Get)  [L52–L61] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to StandardAccountDto [L56]
            └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountDto) [L692]
          └─ calls StandardAccountRepository.ReadQuery [L56]
          └─ query StandardAccount [L56]
            └─ reads_from StandardAccounts
          └─ uses_service StandardAccountRepository
            └─ method ReadQuery [L56]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.StandardAccountRepository.ReadQuery [L9-L42]
        └─ [web] GET /api/accounting/assets/reports/pool  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetDetailReport)  [L153–L166] status=200 [auth=user]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L156]
        └─ [web] GET /api/accounting/reports/pageTypes/admin  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAllAdmin)  [L139–L147] status=200 [auth=user,admin]
          └─ maps_to ReportPageTypeLightAdminDto [L143]
            └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightAdminDto) [L637]
          └─ calls ReportPageTypeRepository.ReadQuery [L143]
          └─ query ReportPageType [L143]
            └─ reads_from ReportPageTypes
          └─ uses_service IJurisdictionService (AddScoped)
            └─ method GetFirmJurisdictions [L142]
              └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetFirmJurisdictions (see previous expansion)
        └─ [web] GET /api/ui/documents/documents/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocument)  [L138–L147] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentDto [L142]
            └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
          └─ calls DocumentRepository.ReadQuery [L142]
          └─ query Document [L142]
            └─ reads_from Documents
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L141]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L68]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L66]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserAsync [L61]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L58]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
        └─ [web] GET /workflow/v1/deliverable-types/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.MinimalQuery)  [L69–L76] status=200
          └─ calls DeliverableTypeRepository.ReadQuery [L73]
          └─ query DeliverableType [L73]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/wholesalers  (Workpapers.Next.API.Controllers.WholesalerController.GetWholesalers)  [L31–L38] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to WholesalerDto [L34]
          └─ uses_service UnitOfWork
            └─ method Table [L34]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/sources/{type}/files  (Dataverse.Api.Controllers.UI.SourcesController.GetAccountingFiles)  [L32–L37] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/reports/images/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Get)  [L40–L44] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetImageQuery -> GetImageQueryHandler [L43]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Images.GetImageQueryHandler.Handle [L23–L57]
              └─ maps_to ImageDto [L43]
                └─ automapper.registration CirrusMappingProfile (Image->ImageDto) [L589]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L53]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IStorageService (InstancePerLifetimeScope)
                └─ method CreateDownloadUrl [L50]
                  └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl (see previous expansion)
              └─ uses_service IControlledRepository<Image> (Scoped (inferred))
                └─ method ReadQuery [L43]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ImageRepository.ReadQuery
              └─ uses_storage IStorageService.CreateDownloadUrl [L50]
        └─ [web] GET /api/sources/{id}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Get)  [L78–L86] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to SourceDto [L81]
            └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
            └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
          └─ calls SourceRepository.ReadQuery [L81]
          └─ query Source [L81]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L81]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/reports/styles/css/branding-css  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetBrandingCss)  [L46–L56] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to ReportSettingsDto [L53]
            └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
          └─ calls ReportSettingsRepository.ReadQuery [L53]
          └─ calls OfficeRepository.ReadQuery [L51]
          └─ query ReportSettings [L53]
            └─ reads_from ReportSettings
          └─ query Office [L51]
            └─ reads_from Offices
          └─ sends_request GetPdfBrandingCssQuery -> GetPdfBrandingCssQueryHandler [L54]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfBrandingCssQueryHandler.Handle [L20–L41]
              └─ uses_service FileManagerService
                └─ method GetCurrentFolder [L31]
                  └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder (see previous expansion)
        └─ [web] GET /api/ui/fyi/groups/{groupId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetGroup)  [L166–L172] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetGroupAsync [L169]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetGroupAsync [L19-L291]
        └─ [web] GET /api/accounting/ledger/master-accounts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.MasterAccountController.GetHeader)  [L31–L37] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to MasterAccountEditingDto [L34]
            └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountEditingDto) [L302]
          └─ calls MasterAccountRepository.ReadQuery [L34]
          └─ query MasterAccount [L34]
            └─ reads_from MasterAccounts
        └─ [web] GET /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSets)  [L43–L62] status=200
          └─ maps_to ProductSetDto [L58]
          └─ uses_service UnitOfWork
            └─ method Table [L58]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ sends_request GetProductSetsQuery -> GetProductSetsQueryHandler [L54]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.ProductSets.GetProductSetsQueryHandler.Handle [L33–L71]
              └─ uses_service UnitOfWork
                └─ method Table [L46]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirm)  [L88–L112] status=200
          └─ maps_to FirmDto [L96]
            └─ converts_to FirmWithStatsDto [L162]
          └─ maps_to FirmWithStatsDto [L96]
          └─ maps_to FirmWithSubscriptionsDto [L95]
            └─ converts_to FirmWithStatsDto [L170]
          └─ uses_service UnitOfWork
            └─ method Table [L95]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method IsSuperUser [L91]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
        └─ [web] GET /api/workpaperitems/byworkbook/unresolved/{workbookId}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorkbookUnresolved)  [L119–L137] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L122]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/gov-link/individual-income-tax-returns/test  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportTest)  [L56–L63] status=200 [AllowAnonymous]
          └─ uses_service TestService
            └─ method GetIncomeTaxReturn [L62]
              └─ ... (no implementation details available)
        └─ [web] GET /api/reportsettings/layoutrules/  (Workpapers.Next.API.Controllers.Reportance.LayoutRulesController.GetLayoutRules)  [L42–L49] status=200
          └─ maps_to LayoutRuleDto [L48]
        └─ [web] GET /api/ui/fyi-elite/{id}/test-connection  (Dataverse.Api.Controllers.UI.FyiEliteController.TestConnection)  [L64–L88] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ calls SyncConfigurationRepository.WriteQuery [L69]
          └─ write SyncConfiguration [L69]
            └─ reads_from SyncConfigurations
          └─ uses_service UserService
            └─ method GetUserAsync [L71]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
          └─ uses_service IDatagetFyiEliteService (AddTransient)
            └─ method TestConnection [L68]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.TestConnection [L13-L53]
        └─ [web] GET /api/accounting/sourcedata/sources/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetForFile)  [L80–L103] status=200 [auth=user]
          └─ maps_to SourceLightDto [L97]
            └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
          └─ calls SourceRepository.ReadQuery [L97]
          └─ query Source [L97]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetAllImplementedApiTypeFeatures [L89]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAllImplementedApiTypeFeatures [L14-L75]
        └─ [web] GET /api/accounting/ledger/source-accounts/mappings  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetSourceAccountMappings)  [L90–L105] status=200 [auth=user]
          └─ calls DatasetRepository.ReadQuery [L98]
          └─ query Dataset [L98]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L97]
        └─ [web] GET /api/templates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplatesController.DownloadTemplate)  [L72–L77] status=200
          └─ sends_request DownloadTemplateQuery -> DownloadTemplateQueryHandler [L75]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.DownloadTemplateQueryHandler.Handle [L24–L60]
              └─ uses_service StorageService
                └─ method CreateDownloadUrl [L58]
                  └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L57]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Template> (Scoped (inferred))
                └─ method ReadQuery [L45]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.TemplateRepository.ReadQuery
              └─ uses_storage StorageService.CreateDownloadUrl [L58]
        └─ [web] GET /audit/find  (Dataverse.Api.Controllers.External.TeamsController.FindAudit)  [L46–L50] status=200
          └─ calls TeamRepository.ReadQuery [L49]
          └─ query Team [L49]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L49]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/tasks  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTaskManager)  [L151–L162] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls TaskRepository.WriteQuery [L153]
          └─ write WorkflowTask [L153]
            └─ reads_from DVS_Tasks
        └─ [web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystem)  [L32–L37] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetExternalReportingSystemQuery -> GetExternalReportingSystemQueryHandler [L36]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemQueryHandler.Handle [L28–L47]
              └─ maps_to ExternalReportingSystemDto [L43]
                └─ automapper.registration DataverseMappingProfile (ExternalReportingSystem->ExternalReportingSystemDto) [L241]
              └─ uses_service UserService
                └─ method IsMaster [L44]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster (see previous expansion)
              └─ uses_service IControlledRepository<ExternalReportingSystem> (Scoped (inferred))
                └─ method ReadQuery [L43]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemRepository.ReadQuery
        └─ [web] GET /api/internal/sync-configuration/all  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetAllTenants)  [L52–L60] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/internal/storage/pending-operations  (Dataverse.Api.Controllers.Internal.StorageController.GetPendingDocumentVersionOperations)  [L209–L216] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetPendingDocumentVersionOperationsQuery -> GetPendingDocumentVersionOperationsQueryHandler [L213]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetPendingDocumentVersionOperationsQueryHandler.Handle [L26–L131]
              └─ calls TenantRepository.ReadTable [L53]
        └─ [web] GET /workpapers/v1/sources/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.GetSourcesMinimalQuery)  [L57–L63] status=200
          └─ calls SourceRepository.ReadQuery [L60]
          └─ query Source [L60]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L60]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCodes)  [L101–L106] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodesQuery -> GetExternalReportingSystemMappingCodesQueryHandler [L105]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesQueryHandler.Handle [L33–L72]
              └─ maps_to ExternalReportingSystemMappingCodeDto [L54]
                └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
              └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
              └─ uses_service UserService
                └─ method IsMaster [L53]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster (see previous expansion)
        └─ [web] GET /api/accounting/divisions/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Setup.DivisionsController.GetAll)  [L56–L65] status=200 [auth=user]
          └─ maps_to DivisionLightDto [L60]
            └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
          └─ calls DivisionRepository.ReadQuery [L60]
          └─ query Division [L60]
            └─ reads_from Divisions
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L59]
        └─ [web] GET /api/users/byfirm/{firmId:Guid}  (Workpapers.Next.API.Controllers.UsersController.GetForFirm)  [L91–L106] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetPagedUsersQuery [L105]
        └─ [web] GET /api/accounting/reports/page-layouts/new-template  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetNewTemplate)  [L78–L82] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetReportPageLayoutQuery -> GetReportPageLayoutsQueryHandler [L81]
        └─ [web] GET /api/accounting/ledger/cashflow/journals/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Get)  [L40–L49] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowJournalDto [L43]
            └─ automapper.registration CirrusMappingProfile (CashflowJournal->CashflowJournalDto) [L508]
          └─ calls CashflowJournalRepository.ReadQuery [L43]
          └─ query CashflowJournal [L43]
            └─ reads_from CashflowJournals
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L46]
        └─ [web] GET /api/ui/fyi/documents/{documentVersionId:guid}/downloadUrl  (Dataverse.Api.Controllers.UI.FyiController.GetDocumentDownloadUrl)  [L106–L112] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetDocumentDownloadUrlAsync [L109]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentDownloadUrlAsync [L19-L291]
        └─ [web] GET /core/v1/offices/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.Get)  [L52–L57] status=200
          └─ maps_to OfficeDto [L55]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
            └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
          └─ calls OfficeRepository.ReadQuery [L55]
          └─ query Office [L55]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L55]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/sources/{type}/taxonomy  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTaxonomy)  [L441–L447] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetBinderTaxonomyQuery -> GetBinderTaxonomyQueryHandler [L444]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderTaxonomyQueryHandler.Handle [L32–L113]
              └─ calls SourceAccountRepository.ReadQuery [L92]
              └─ uses_service ConnectionApiService
                └─ method GetApiMethods [L86]
                  └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L58]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L56]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/accounting/reports/styles/word/download/{reportStyleId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWordController.DownloadTemplate)  [L55–L79] status=200 [auth=Authentication.AdminPolicy]
          └─ calls ReportStyleRepository.ReadQuery [L59]
          └─ query ReportStyle [L59]
            └─ reads_from ReportStyles
        └─ [web] GET /api/super/smart-workpapers/{tenantId}/tenant-creation-info  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.GetTenantCreationInfo)  [L51–L61] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_client WorkpapersClient [L60]
            └─ calls Search [L60]
              └─ remote_endpoint_expansion_suppressed (see previous expansion)
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/workspaces/{workspaceId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaceFolders)  [L208–L216] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageWorkspaceChildrenQuery -> GetIManageWorkspaceChildrenQueryHandler [L215]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspaceChildrenQueryHandler.Handle [L22–L38]
              └─ uses_service IManageService
                └─ method GetWorkspaceFolders [L33]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaceFolders [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/fyi/documents/{documentVersionId:guid}/authorise-upload  (DataGet.Api.Controllers.Integrations.FyiController.AuthoriseUpload)  [L144–L152] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentUploadAuthorisationQuery -> GetDocumentUploadAuthorisationQueryHandler [L149]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentUploadAuthorisationQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method AuthoriseUpload [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.AuthoriseUpload [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/connections/reportance/files  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAccountingFiles)  [L28–L34] status=200
          └─ sends_request GetFilesQuery -> GetFilesQueryHandler [L31]
            └─ handled_by Workpapers.Next.CirrusServices.Queries.GetFilesQueryHandler.Handle [L12–L39]
              └─ uses_service IMapToNew<FileSearchResult, AccountingFileDto>
                └─ method Map [L37]
                  └─ ... (no implementation details available)
              └─ uses_service CirrusHttp
                └─ method GetHttpResponseAsync [L35]
                  └─ ... (no implementation details available)
              └─ uses_service CirrusConfig
                └─ method GetBaseUrl [L27]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/reportsettings/reporttemplates/  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Get)  [L52–L62] status=200
          └─ maps_to ReportTemplate [L56]
            └─ converts_to ReportTemplateDto [L534]
            └─ converts_to ReportTemplateModifiedInfoDto [L542]
            └─ converts_to ReportTemplateLightDto [L546]
            └─ converts_to ReportTemplate [L23]
          └─ uses_service UnitOfWork
            └─ method Table [L56]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L55]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
        └─ [web] GET /api/super/storage/validate-document/{documentId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.ValidateDocument)  [L57–L63] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request ValidateDocumentCommand -> ValidateDocumentCommandHandler [L60]
            └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
              └─ calls DocumentVersionRepository.ReadQuery [L41]
              └─ uses_service DocumentServiceFactory
                └─ method GetDefaultColdStorage [L52]
                  └─ implementation DocumentServiceFactory.GetDefaultColdStorage
        └─ [web] GET /api/internal/tasks/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTask)  [L40–L46] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to WorkflowTaskDto [L43]
            └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L371]
            └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
          └─ calls TaskRepository.ReadQuery [L43]
          └─ query WorkflowTask [L43]
            └─ reads_from DVS_Tasks
        └─ [web] GET /api/connections/xero/wages  (Workpapers.Next.API.Controllers.Connections.XeroController.GetWages)  [L86–L92] status=200
        └─ [web] GET /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Get)  [L137–L174] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderDto [L151]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderDto) [L450]
            └─ automapper.registration ExternalApiMappingProfile (Binder->BinderDto) [L58]
          └─ maps_to BinderUltraLightDto [L145]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
          └─ calls BinderRepository.ReadQuery [L145]
          └─ query Binder [L145]
            └─ reads_from Binders
          └─ uses_service ICirrusQueryService (AddScoped)
            └─ method GetDatasetsForFile [L159]
              └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDatasetsForFile [L18-L250]
                └─ uses_service CirrusHttp
                  └─ method GetAccounts [L33]
                    └─ ... (no implementation details available)
                └─ uses_service CirrusConfig
                  └─ method GetAccounts [L31]
                    └─ ... (no implementation details available)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L141]
        └─ [web] GET /api/ui/workflow/job-filter-sets/{id:Guid}/can-i-save  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.CanISave)  [L75–L83] status=200 [auth=Authentication.UserPolicy]
          └─ calls JobFilterSetRepository.WriteQuery [L78]
          └─ write JobFilterSet [L78]
            └─ reads_from JobFilterSets
          └─ sends_request CanIEditJobFilterSet -> CanIEditJobFilterSetHandler [L82]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
              └─ uses_service UserService
                └─ method GetUserAsync [L38]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
        └─ [web] GET /api/accounting/ledger/accounts/child-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAllChildAccounts)  [L90–L101] status=200 [auth=user]
          └─ maps_to AccountLightDto [L95]
            └─ automapper.registration CirrusMappingProfile (Account->AccountLightDto) [L313]
          └─ calls AccountRepository.ReadQuery [L95]
          └─ query Account [L95]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L94]
        └─ [web] GET /api/accounting/datasets/{id}  (Cirrus.Api.Controllers.Accounting.DatasetsController.Get)  [L51–L58] status=200 [auth=user]
          └─ maps_to DatasetDto [L55]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
            └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
          └─ calls DatasetRepository.ReadQuery [L55]
          └─ query Dataset [L55]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L54]
        └─ [web] GET /api/ui/contacts/{id}/children  (Dataverse.Api.Controllers.UI.Core.ContactsController.GetChildContacts)  [L95–L108] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ContactSearchDto [L98]
            └─ automapper.registration DataverseMappingProfile (Contact->ContactSearchDto) [L160]
          └─ calls ContactRepository.ReadQuery [L98]
          └─ query Contact [L98]
            └─ reads_from DVS_Clients
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L100]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L100]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
                └─ uses_service User
                  └─ method GetUserId [L43]
                    └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                    └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
                └─ uses_service Guid?
                  └─ method GetUserId [L40]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/internal/Propagator/workflow-changed-job-status  (Dataverse.Api.Controllers.Internal.PropagatorController.GetWorkflowJobs)  [L150–L169] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls JobRepository.ReadQuery [L153]
          └─ query Job [L153]
            └─ reads_from Jobs
          └─ uses_service JobRepository
            └─ method ReadQuery [L153]
              └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery [L8-L46]
        └─ [web] GET /api/companies-house/company/{companyNumber}/appointments/{appointmentId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficer)  [L141–L149] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyOfficerQuery -> GetOfficerQueryHandler [L146]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerQueryHandler.Handle [L16–L28]
        └─ [web] GET /api/accounting/reports/published/template/{templateId:guid}  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List)  [L60–L77] status=200 [auth=user]
          └─ maps_to PublishedReportBatchDto [L72]
            └─ automapper.registration CirrusMappingProfile (PublishedReportBatch->PublishedReportBatchDto) [L601]
          └─ calls PublishedReportBatchRepository.ReadQuery [L66]
          └─ query PublishedReportBatch [L66]
            └─ reads_from PublishedReportBatches
          └─ uses_service IRequestProcessor (InstancePerDependency)
            └─ method ProcessAsync [L70]
              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
        └─ [web] GET /api/ui/workflow/reports/find  (Dataverse.Api.Controllers.UI.Workflow.WorkflowReportsController.GetReport)  [L28–L47] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindJobsReportDataQuery [L46]
        └─ [web] GET /api/super/users/export/excel/download  (Dataverse.Api.Controllers.Super.Core.UsersController.ExportToExcel)  [L281–L296] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserExportProfileDto [L285]
            └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L109]
          └─ calls UserRepository.ReadQuery [L285]
          └─ query User [L285]
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L291]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
          └─ uses_service UserRepository
            └─ method ReadQuery [L285]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L294]
          └─ sends_request ExportUsersToExcelCommand -> ExportUsersToExcelCommandHandler [L292]
            └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
              └─ uses_service UserExcelWriter
                └─ method WriteAsync [L38]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/ui/documents/documents/{id}/meta-data  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetMetaData)  [L169–L187] status=200 [auth=Authentication.UserPolicy]
          └─ calls DocumentVersionRepository.ReadQuery [L174]
          └─ query DocumentVersion [L174]
            └─ reads_from DocumentVersions
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L172]
        └─ [web] GET /api/accounting/ledger/journals/for-dataset/{datasetId:guid}/report/profit-reconciliation  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetProfitReconciliationReport)  [L119–L123] status=200 [auth=user]
        └─ [web] GET /api/ui/offices/{id}/office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeUsers)  [L139–L150] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to OfficeUserWithUserDto [L142]
          └─ calls OfficeRepository.ReadQuery [L142]
          └─ query Office [L142]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L142]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/offices/{id:Guid}/users  (Workpapers.Next.API.Controllers.OfficeController.GetOfficeUsers)  [L81–L91] status=200
          └─ maps_to UserOfficeDto [L84]
          └─ uses_service UnitOfWork
            └─ method Table [L84]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/reportance/cirrus/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.CirrusController.StarterDownload)  [L71–L99] status=200
          └─ uses_client KeenClient [L90]
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L97]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L75]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L97]
          └─ sends_request GetProductQuery -> GetProductQueryHandler [L82]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
              └─ uses_service UnitOfWork
                └─ method Table [L71]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/internal/storage/old-hot-documents/{jobId:Guid}  (Dataverse.Api.Controllers.Internal.StorageController.GetOldHotDocuments)  [L168–L207] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_cache IDistributedCache.GetRecordAsync [read] [L204]
          └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L171]
          └─ uses_service RequiredService
            └─ method AssignActiveTenant [L182]
              └─ ... (no implementation details available)
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L176]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /workpapers/v1/binders/{binderId:Guid}/trial-balance  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetIndexAccountBalanceInfo)  [L108–L114] status=200
          └─ uses_service IWorkpapersProxyService (AddScoped)
            └─ method Get [L111]
              └─ implementation Workpapers.Next.API.External.Features.WorkpapersProxy.WorkpapersProxyService.Get [L13-L83]
                └─ uses_client WorkpapersClient [L31]
                └─ uses_service WorkpapersClient
                  └─ method Get [L31]
                    └─ ... (no implementation details available)
        └─ [web] GET /core/v1/users/  (Dataverse.Api.External.Controllers.v1.Core.UsersController.MinimalQuery)  [L84–L90] status=200
          └─ calls UserRepository.ReadQuery [L87]
          └─ query User [L87]
          └─ uses_service UserRepository
            └─ method ReadQuery [L87]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ledger-reports/{binderColumnDataId:Guid}/source-accounts  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetLedgerForSourceAccounts)  [L80–L114] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderColumnDataRepository.ReadQuery [L97]
          └─ query BinderColumnData [L97]
            └─ reads_from BinderColumnData
          └─ sends_request GetGeneralLedgerBySourceAccountQuery -> GetGeneralLedgerBySourceAccountQueryHandler [L112]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
              └─ maps_to SourceAccountMappingDto [L201]
                └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
              └─ maps_to SourceLightDto [L181]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L206]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service ApiService (SingleInstance)
                └─ method GetFeatures [L186]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method Execute [L149]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
                    └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
                      └─ calls SourceAccountRepository.ReadQuery [L151]
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L136]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L121]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service GetGeneralLedgerForDatesQuery
                └─ method Execute [L79]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQuery.Execute
                    └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQueryHandler.Handle [L38–L87]
                      └─ calls JournalRepository.ReadQuery [L53]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L101]
        └─ [web] GET /api/accounting/workpapers/features  (Cirrus.Api.Controllers.Accounting.WorkpapersController.GetFeatures)  [L35–L62] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service WorkpapersService
            └─ method Get [L43]
              └─ implementation Cirrus.Central.Features.MachineToMachine.WorkpapersService.Get [L12-L30]
          └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L38]
            └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method Process [L51]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.Process [L7-L35]
              └─ logs ILogger<GetFirmForCurrentRequestQueryHandler> [Warning] [L47]
        └─ [web] GET /api/internal/document-statuses/default  (Dataverse.Api.Controllers.Internal.Documents.DocumentStatusesController.GetDefault)  [L32–L42] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to DocumentStatusDto [L35]
            └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
          └─ calls DocumentStatusRepository.ReadQuery [L35]
          └─ query DocumentStatus [L35]
            └─ reads_from DVS_DocumentStatuses
        └─ [web] GET /audit/find  (Dataverse.Api.Controllers.External.ClientsController.FindAudit)  [L46–L50] status=200
          └─ uses_client ClientRepository [L49]
        └─ [web] GET /core/v1/clients/groups/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupsWithChildrenQuery)  [L227–L235] status=200
          └─ uses_client ClientRepository [L230]
        └─ [web] GET /api/officeUsers/foroffice/{officeId:Guid}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.GetAllForOffice)  [L56–L67] status=200 [auth=user]
          └─ maps_to OfficeUserWithUserDto [L61]
            └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserWithUserDto) [L131]
          └─ calls OfficeUserRepository.ReadQuery [L61]
          └─ query OfficeUser [L61]
            └─ reads_from OfficeUsers
        └─ [web] GET /api/internal/webhooks/{hookId:Guid}  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Get)  [L44–L50] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to WebhookDto [L47]
            └─ automapper.registration DataverseMappingProfile (Webhook->WebhookDto) [L458]
            └─ automapper.registration ExternalApiMappingProfile (Webhook->WebhookDto) [L181]
          └─ calls WebhookRepository.ReadQuery [L47]
          └─ query Webhook [L47]
            └─ reads_from Webhooks
        └─ [web] GET /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.GetHelpContent)  [L41–L48] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to TemplateHelpContentDto [L44]
            └─ automapper.registration WorkpapersMappingProfile (TemplateHelpContent->TemplateHelpContentDto) [L326]
          └─ calls TemplateHelpContentRepository.ReadQuery [L44]
          └─ query TemplateHelpContent [L44]
            └─ reads_from TemplateHelpContents
        └─ [web] GET /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetWorkpaperRecordTemplateParams)  [L231–L251] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L236]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/imanage/folders/{folderId}/subfolders  (Dataverse.Api.Controllers.UI.IManageController.GetSubFolders)  [L217–L232] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L220]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L220]
          └─ query IntegrationSettings [L220]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetSubFoldersAsync [L226]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetSubFoldersAsync [L19-L225]
        └─ [web] GET /api/templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.Get)  [L63–L70] status=200
          └─ maps_to TemplateDto [L66]
          └─ uses_service UnitOfWork
            └─ method Table [L66]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/support-users/available-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.GetAvailableSupportUsersAsync)  [L46–L57] status=200 [auth=Authentication.AdminPolicy]
          └─ calls TenantRepository.ReadTable [L51]
          └─ query Tenant [L51]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L51]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/fyi/categories  (DataGet.Api.Controllers.Integrations.FyiController.GetCategories)  [L67–L76] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCategoriesQuery -> GetCategoriesQueryHandler [L72]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoriesQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetCategories [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCategories [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /audit/find  (Dataverse.Api.Controllers.External.UsersController.FindAudit)  [L47–L51] status=200
          └─ calls UserRepository.ReadQuery [L50]
          └─ query User [L50]
          └─ uses_service UserRepository
            └─ method ReadQuery [L50]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/clients/exist-for-firm  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForClients)  [L129–L137] status=200 [auth=Authentication.UserPolicy]
          └─ uses_client ClientRepository [L132]
        └─ [web] GET /api/ui/workflow/tasks/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetTask)  [L88–L95] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to WorkflowTaskDto [L91]
          └─ calls TaskRepository.ReadQuery [L91]
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L93]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L93]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service TaskRepository
            └─ method ReadQuery [L91]
              └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery (see previous expansion)
        └─ [web] GET /workflow/v1/jobs/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditQuery)  [L118–L123] status=200
          └─ maps_to EntityAuditDto [L121]
          └─ calls JobRepository.ReadQuery [L121]
          └─ query Job [L121]
            └─ reads_from Jobs
        └─ [web] GET /api/accounting/sourcedata/sources/{id}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Get)  [L55–L64] status=200 [auth=user]
          └─ maps_to UserUltraLightDto [L61]
            └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
          └─ maps_to SourceDto [L58]
            └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
          └─ calls UserRepository.ReadQuery [L61]
          └─ calls SourceRepository.ReadQuery [L58]
          └─ query User [L61]
          └─ query Source [L58]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetFeatures [L62]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
        └─ [web] GET /api/gov-link/individual-income-tax-returns/test/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparisonTest)  [L66–L73] status=200 [AllowAnonymous]
          └─ uses_service TestService
            └─ method GetProfileComparison [L72]
              └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetHeader)  [L134–L141] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardHeaderDto [L137]
          └─ calls StandardAccountRepository.ReadQuery [L137]
          └─ query StandardAccount [L137]
            └─ reads_from StandardAccounts
        └─ [web] GET /api/companies-house-gateway/members-register-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetMembersRegisterDataAsync)  [L38–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetMembersRegisterDataQuery -> GetMembersRegisterDataQueryHandler [L41]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetMembersRegisterDataQueryHandler.Handle [L33–L62]
              └─ maps_to MembersRegisterDataResponseDto [L60]
              └─ uses_client CompaniesHouseInputGatewayClient [L56]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L56]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L55]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/ledger/cashflow/lines/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.GetLines)  [L53–L63] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowLineDto [L58]
            └─ automapper.registration CirrusMappingProfile (CashflowLine->CashflowLineDto) [L470]
          └─ calls CashflowLineRepository.ReadQuery [L58]
          └─ query CashflowLine [L58]
            └─ reads_from CashflowLines
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L56]
        └─ [web] GET /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.GetAll)  [L48–L54] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ReportWatermarkDto [L51]
            └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
          └─ calls WatermarkRepository.ReadQuery [L51]
          └─ query ReportWatermark [L51]
            └─ reads_from ReportWatermarks
        └─ [web] GET /api/super/data-cloud/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.DataCloud.DataCloudSubscriptionController.GetSubscription)  [L42–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L47]
          └─ calls TenantRepository.ReadTable [L47]
          └─ query Tenant [L47]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L47]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/accounting/ledger/accounts/{fileId:Guid}/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.RefreshAccountHierarchy)  [L212–L247] status=200 [auth=user]
          └─ calls AccountRepository.ReadQuery [L224]
          └─ calls FileRepository.ReadQuery [L218]
          └─ query Account [L224]
          └─ query File [L218]
            └─ reads_from Files
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L215]
        └─ [web] GET /api/ui/documents/statuses  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAll)  [L33–L42] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentStatusDto [L36]
            └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
          └─ calls DocumentStatusRepository.ReadQuery [L36]
          └─ query DocumentStatus [L36]
            └─ reads_from DVS_DocumentStatuses
        └─ [web] GET /api/ui/tax-agents/  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.GetAll)  [L32–L38] status=200 [auth=Authentication.UserPolicy]
          └─ calls TaxAgentRepository.ReadQuery [L35]
          └─ query TaxAgent [L35]
            └─ reads_from TaxAgents
        └─ [web] GET /api/workpapers/auto-reconcile/creditors  (Cirrus.Api.Controllers.Workpapers.AutoReconcileController.GetCreditors)  [L45–L51] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetApiMethods [L49]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
        └─ [web] GET /api/accounting/tradingAccounts/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.GetAll)  [L56–L65] status=200 [auth=user]
          └─ maps_to TradingAccountDto [L60]
            └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountDto) [L227]
          └─ calls TradingAccountRepository.ReadQuery [L60]
          └─ query TradingAccount [L60]
            └─ reads_from TradingAccounts
        └─ [web] GET /api/super/users/{id:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.UsersController.GetUserAudit)  [L184–L210] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls UserRepository.ReadQuery [L188]
          └─ query User [L188]
          └─ uses_service UserRepository
            └─ method ReadQuery [L188]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirms)  [L56–L74] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetPagedFirmsQuery -> GetPagedFirmsQueryHandler [L70]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetPagedFirmsQueryHandler.Handle [L38–L81]
              └─ uses_service UnitOfWork
                └─ method Table [L51]
                  └─ implementation UnitOfWork.Table (see previous expansion)
          └─ sends_request GetFirmsQuery -> GetFirmsQueryHandler [L68]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetFirmsQueryHandler.Handle [L21–L39]
              └─ maps_to FirmDto [L34]
                └─ converts_to FirmWithStatsDto [L162]
              └─ uses_service UnitOfWork
                └─ method Table [L34]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/reportsettings/policies/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicy)  [L46–L56] status=200
          └─ maps_to Policy [L49]
            └─ converts_to PolicyDto [L789]
            └─ converts_to PolicyLightDto [L792]
            └─ converts_to PolicyLightWithSuiteVariantsDto [L794]
            └─ converts_to Policy [L18]
          └─ uses_service UserService
            └─ method GetFirmId [L52]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L49]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /workflow/v1/job-statuses/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditsQuery)  [L102–L108] status=200
          └─ maps_to EntityAuditDto [L105]
          └─ calls JobStatusRepository.ReadQuery [L105]
          └─ query JobStatus [L105]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/stats/newusers  (Workpapers.Next.API.Controllers.StatsController.NewUsers)  [L188–L200] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetNewUsersQuery -> GetNewUsersQueryHandler [L197]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetNewUsersQueryHandler.Handle [L37–L91]
              └─ uses_service UnitOfWork
                └─ method Table [L53]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFileGet)  [L22–L30] status=200
          └─ sends_request UploadChunkExistsQuery -> UploadChunkExistsQueryHandler [L25]
            └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkExistsQueryHandler.Handle [L24–L62]
              └─ uses_service IStorageService (InstancePerLifetimeScope)
                └─ method BlockExists [L50]
                  └─ implementation DataGet.Data.BlobStorage.StorageService.BlockExists [L11-L116]
                    └─ uses_service RequestContextProvider
                      └─ method GetContainer [L89]
                        └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
              └─ uses_storage IStorageService.BlockExists [L50]
        └─ [web] GET /api/fyi/groups  (DataGet.Api.Controllers.Integrations.FyiController.GetGroups)  [L197–L206] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetGroupsQuery -> GetGroupsQueryHandler [L202]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupsQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetGroups [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetGroups [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/ui/offices/{id}/find-office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.FindOfficeUsers)  [L109–L130] status=200 [auth=Authentication.UserPolicy]
          └─ calls OfficeRepository.ReadQuery [L116]
          └─ query Office [L116]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L116]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/contacts/  (Dataverse.Api.Controllers.UI.Core.ContactsController.Search)  [L55–L81] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindContactsQuery -> FindContactsQueryHandler [L69]
            └─ handled_by Dataverse.ApplicationService.Queries.Contacts.FindContactsQueryHandler.Handle [L62–L131]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L85]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L85]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<Contact> (Scoped (inferred))
                └─ method ReadQuery [L83]
                  └─ implementation Dataverse.Data.Repository.Clients.ContactRepository.ReadQuery
        └─ [web] GET /api/super/friction-map/findAll  (Dataverse.Api.Controllers.Super.FrictionMapController.FindFrictionMap)  [L24–L28] status=200 [auth=Authentication.MasterPolicy]
        └─ [web] GET /api/documents/upload  (Cirrus.Api.Controllers.UploadController.UploadFileGet)  [L23–L29] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAll)  [L83–L95] status=200 [auth=user]
          └─ maps_to ReportPageTypeLightWithContentFieldsDto [L90]
            └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightWithContentFieldsDto) [L635]
          └─ calls ReportPageTypeRepository.ReadQuery [L90]
          └─ query ReportPageType [L90]
            └─ reads_from ReportPageTypes
          └─ uses_service IJurisdictionService (AddScoped)
            └─ method GetUserJurisdictions [L88]
              └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetUserJurisdictions [L7-L27]
                └─ uses_service FirmSettingsService
                  └─ method GetFirmJurisdictions [L18]
                    └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetFirmJurisdictions (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualBeneficialOwner)  [L284–L292] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscIndividualBeneficialOwnerQuery -> GetCompanyPscIndividualBeneficialOwnerQueryHandler [L289]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualBeneficialOwnerQueryHandler.Handle [L19–L31]
        └─ [web] GET /workpapers/v1/worksheets/{worksheetId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.Get)  [L45–L51] status=200
          └─ maps_to WorksheetDto [L48]
            └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetDto) [L507]
            └─ automapper.registration ExternalApiMappingProfile (Worksheet->WorksheetDto) [L134]
          └─ calls WorksheetRepository.ReadQuery [L48]
          └─ query Worksheet [L48]
            └─ reads_from Worksheets
        └─ [web] GET /api/ato/gov-link/individual-income-tax-returns/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshIndividualIncomeTaxReturnSummary)  [L171–L178] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetIndividualIncomeTaxReturnSummaryQuery -> GetIndividualIncomeTaxReturnSummaryQueryHandler [L175]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnSummaryQueryHandler.Handle [L32–L72]
              └─ uses_client DataGetClient [L59]
                └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetIndividualIncomeTaxReturnSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/individual-income-tax-returns/summary  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportSummary)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ uses_service AtoService
                        └─ method GetIncomeTaxReportSummary [L39]
                          └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxReportSummary [L12-L145]
              └─ uses_service DataGetClient
                └─ method GetIndividualIncomeTaxReturnSummaryAsync [L59]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetIndividualIncomeTaxReturnSummaryAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L51]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /webhooks/v1/webhooks/{webhookId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.Get)  [L57–L62] status=200 [auth=Authentication.CoreRead]
          └─ maps_to WebhookDto [L60]
            └─ automapper.registration DataverseMappingProfile (Webhook->WebhookDto) [L458]
            └─ automapper.registration ExternalApiMappingProfile (Webhook->WebhookDto) [L181]
          └─ calls WebhookRepository.ReadQuery [L60]
          └─ query Webhook [L60]
            └─ reads_from Webhooks
        └─ [web] GET /api/companies-house/disqualified-officers/natural/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetNaturalDisqualification)  [L223–L231] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetNaturalDisqualificationQuery -> GetNaturalDisqualificationQueryHandler [L228]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetNaturalDisqualificationQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/companies-house/company/{companyNumber}/registers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisters)  [L151–L159] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyRegistersQuery -> GetCompanyRegistersQueryHandler [L156]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegistersQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/ui/documents/tags/  (Dataverse.Api.Controllers.UI.Documents.DocumentTagsController.Find)  [L26–L37] status=200 [auth=Authentication.UserPolicy]
          └─ calls DocumentTagRepository.ReadQuery [L33]
          └─ query DocumentTag [L33]
            └─ reads_from DocumentTags
        └─ [web] GET /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Get)  [L48–L54] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TeamDto [L51]
            └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
            └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
          └─ calls TeamRepository.ReadQuery [L51]
          └─ query Team [L51]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L51]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/teams/{id}/team-users  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetTeamUsers)  [L66–L75] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TeamUserWithUserDto [L69]
          └─ calls TeamRepository.ReadQuery [L69]
          └─ query Team [L69]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L69]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Search)  [L42–L51] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request FindTenantsQuery -> FindTenantsQueryHandler [L50]
            └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
              └─ uses_service DataverseService
                └─ method Get [L50]
                  └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get (see previous expansion)
        └─ [web] GET /api/stats/binder-report/{firmId}  (Workpapers.Next.API.Controllers.StatsController.BinderRecordTemplates)  [L265–L280] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request BinderReportBaseQuery [L277]
        └─ [web] GET /api/ui/workflow/tasks/find  (Dataverse.Api.Controllers.UI.Workflow.TasksController.FindTasks)  [L44–L57] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindTasksQuery -> FindTasksQueryHandler [L54]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindTasksQueryHandler.Handle [L42–L116]
              └─ calls TaskRepository.ReadQuery [L59]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L68]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L68]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/cloud-capcha/access-info  (DataGet.Api.Controllers.Integrations.CloudCapchaController.GetAccessInfo)  [L77–L91] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service IApiTokenService (InstancePerLifetimeScope)
            └─ method GetTokenAsync [L82]
              └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync [L11-L61]
                └─ uses_service ConnectionContextProvider
                  └─ method GetTokenAsync [L28]
                    └─ ... (no implementation details available)
                └─ uses_service IControlledRepository<ApiToken> (Scoped (inferred))
                  └─ method GetTokenAsync [L26]
                    └─ implementation DataGet.Data.Repository.Connections.ApiTokenRepository.GetTokenAsync
                └─ maps_to ExternalApiToken [L30]
        └─ [web] GET /api/accounting/reports/styles/fonts/{fontName}/url  (Cirrus.Api.Controllers.Accounting.Reports.Styles.FontsController.GetUploadedFontUrl)  [L24–L30] status=200
          └─ sends_request GetUploadedFontUrlQuery -> GetUploadedFontUrlQueryHandler [L28]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetUploadedFontUrlQueryHandler.Handle [L23–L41]
              └─ uses_service UploadedFontsConfiguration
                └─ method UrlExpiryMin [L37]
                  └─ ... (no implementation details available)
              └─ uses_service IStorageService (InstancePerLifetimeScope)
                └─ method CreateDownloadUrl [L37]
                  └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl (see previous expansion)
              └─ uses_storage IStorageService.CreateDownloadUrl [L37]
        └─ [web] GET /api/connections/bgl360/funds/{fundId}  (Workpapers.Next.API.Controllers.Connections.Bgl360Controller.GetFund)  [L33–L39] status=200
        └─ [web] GET /api/gov-link/client-accounts/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetail)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetClientAccountTransactions [L27]
              └─ implementation GovLink.Application.Services.AtoService.GetClientAccountTransactions [L12-L145]
        └─ [web] GET /api/ui/documents/statuses/many  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetMany)  [L55–L65] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentStatusDto [L58]
            └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
          └─ calls DocumentStatusRepository.ReadQuery [L58]
          └─ query DocumentStatus [L58]
            └─ reads_from DVS_DocumentStatuses
        └─ [web] GET /api/super/smart-workpapers  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.GetRegionCodeAsync)  [L143–L146] status=200 [auth=Authentication.MasterPolicy]
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L145]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /api/matching-rule-sets/{id}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Get)  [L40–L46] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to MatchingRuleSetDto [L43]
            └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
          └─ calls MatchingRuleSetRepository.ReadQuery [L43]
          └─ query MatchingRuleSet [L43]
            └─ reads_from MatchingRuleSets
          └─ uses_service MatchingRuleSetRepository
            └─ method ReadQuery [L43]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.ReadQuery [L8-L38]
        └─ [web] GET /api/hmrc/feedback  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetFraudHeaderFeedback)  [L89–L96] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetHmrcFraudHeaderFeedbackQuery -> GetHmrcFraudHeaderFeedbackQueryHandler [L93]
            └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.GetHmrcFraudHeaderFeedbackQueryHandler.Handle [L14–L27]
              └─ uses_client HmrcClient [L25]
              └─ uses_service HmrcClient
                └─ method GetFraudHeaderFeedback [L25]
                  └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcClient.GetFraudHeaderFeedback [L35-L298]
        └─ [web] GET /api/clients/{id:guid}/binders  (Workpapers.Next.API.Controllers.ClientsController.GetChildrenAndBinders)  [L78–L122] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderListItemClientDto [L102]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemClientDto) [L438]
          └─ maps_to ClientWithBindersDto [L95]
            └─ automapper.registration WorkpapersMappingProfile (Client->ClientWithBindersDto) [L72]
          └─ uses_client ClientRepository [L95]
          └─ uses_client ClientRepository [L87]
          └─ calls BinderRepository.ReadQuery [L102]
          └─ query Binder [L102]
            └─ reads_from Binders
        └─ [web] GET /api/journals/for-binder-column/{binderColumnId:guid}/report  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetJournalReport)  [L164–L168] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetJournalReportQuery -> GetJournalReportQueryHandler [L167]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportQueryHandler.Handle [L42–L90]
              └─ uses_service GetJournalsWithTransactionsQuery
                └─ method Execute [L72]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L63]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L57]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/accounting/reports/notes/reporting-suites/admin  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetAll)  [L41–L55] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ maps_to ReportingSuiteLightDto [L45]
            └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteLightDto) [L760]
          └─ calls ReportingSuiteRepository.ReadQuery [L45]
          └─ query ReportingSuite [L45]
            └─ reads_from PolicySuites
          └─ uses_service IJurisdictionService (AddScoped)
            └─ method GetFirmJurisdictions [L44]
              └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetFirmJurisdictions (see previous expansion)
        └─ [web] GET /api/ui/imanage/test-connection  (Dataverse.Api.Controllers.UI.IManageController.TestConnection)  [L112–L118] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method TestConnection [L115]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.TestConnection [L19-L225]
        └─ [web] GET /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Get)  [L54–L63] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to MatchingRuleDto [L58]
            └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
          └─ calls MatchingRuleRepository.ReadQuery [L58]
          └─ query MatchingRule [L58]
            └─ reads_from MatchingRules
          └─ uses_service MatchingRuleRepository
            └─ method ReadQuery [L58]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.ReadQuery [L9-L41]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetWorksheetParams)  [L437–L446] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L439]
          └─ query Binder [L439]
            └─ reads_from Binders
        └─ [web] GET /api/workpaperitems/byworkbook/{workbookId}/{worksheetId}/byrangename/{rangeName}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetByRange)  [L151–L164] status=200
          └─ maps_to WorkpaperItemDto [L154]
          └─ uses_service UnitOfWork
            └─ method Table [L154]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.FindDocuments)  [L80–L136] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindDocumentsQuery -> FindDocumentsQueryHandler [L110]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentsQueryHandler.Handle [L77–L428]
              └─ uses_service IControlledRepository<DocumentTag> (Scoped (inferred))
                └─ method ReadQuery [L373]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentTagRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L104]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserAsync [L104]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L103]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
        └─ [web] GET /api/ato/export-pdf  (Workpapers.Next.API.Controllers.Workpapers.AtoController.ExportIcaItaToPdf)  [L90–L108] status=200 [auth=AuthorizationPolicies.User]
        └─ [web] GET /api/sources/{type}/journal-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetJournalUrl)  [L455–L476] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L458]
          └─ query Binder [L458]
            └─ reads_from Binders
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L472]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/ui/fyi/users  (Dataverse.Api.Controllers.UI.FyiController.GetUsers)  [L174–L180] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetUsersAsync [L177]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetUsersAsync [L19-L291]
        └─ [web] GET /api/useroffices/foroffice/{officeId:Guid}  (Workpapers.Next.API.Controllers.UserOfficesController.GetAllForOffice)  [L64–L74] status=200
          └─ maps_to UserOfficeDto [L67]
          └─ uses_service UnitOfWork
            └─ method Table [L67]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/binders/download-binder/{binderId}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Download)  [L176–L187] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderUltraLightDto [L182]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
          └─ calls BinderRepository.ReadQuery [L182]
          └─ query Binder [L182]
            └─ reads_from Binders
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L180]
        └─ [web] GET /api/sources/taxonomy  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinderTaxonomy)  [L433–L439] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetBinderTaxonomyQuery -> GetBinderTaxonomyQueryHandler [L436]
        └─ [web] GET /api/accounting/ledger/import-runs/for-dataset  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.GetForDataset)  [L68–L88] status=200 [auth=user]
          └─ maps_to UserUltraLightDto [L80]
            └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
          └─ maps_to ImportRunLightDto [L73]
            └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunLightDto) [L513]
          └─ calls UserRepository.ReadQuery [L80]
          └─ calls ImportRunRepository.ReadQuery [L73]
          └─ query User [L80]
          └─ query ImportRun [L73]
            └─ reads_from ImportRuns
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L71]
        └─ [web] GET /api/ui/users  (Dataverse.Api.Controllers.UI.Core.UsersController.Search)  [L125–L167] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindUsersLightQuery [L164]
          └─ sends_request FindUsersUltraLightWithStandardHoursQuery [L160]
          └─ sends_request FindUsersUltraLightQuery -> FindSingleTenantCentralUsersQueryHandler [L154]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindSingleTenantCentralUsersQueryHandler.Handle [L85–L194]
              └─ calls UserRepository.ReadQuery [L125]
          └─ sends_request FindUsersWithLicensesQuery [L148]
        └─ [web] GET /api/matching-rules/rule-set/{ruleSetId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.GetAllFromSet)  [L72–L81] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to MatchingRuleDto [L76]
            └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
          └─ calls MatchingRuleRepository.ReadQuery [L76]
          └─ query MatchingRule [L76]
            └─ reads_from MatchingRules
          └─ uses_service MatchingRuleRepository
            └─ method ReadQuery [L76]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/ledger/journals/for-dataset/{datasetId}/report/Excel  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetExcelJournalReport)  [L129–L134] status=200 [auth=user]
          └─ sends_request GetJournalReportForExcelQuery -> GetJournalReportForExcelQueryHandler [L132]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportForExcelQueryHandler.Handle [L59–L253]
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L79]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L76]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/binder-column-template-sets/{id:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Get)  [L35–L39] status=200
          └─ sends_request GetBinderColumnTemplateSet -> GetBinderColumnTemplateSetHandler [L38]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetHandler.Handle [L22–L40]
              └─ maps_to BinderColumnTemplateSetDto [L35]
                └─ automapper.registration WorkpapersMappingProfile (BinderColumnTemplateSet->BinderColumnTemplateSetDto) [L368]
              └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
                └─ method ReadQuery [L35]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
        └─ [web] GET /api/workpaper-record-templates/{id:Guid}/sections  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetTemplateSections)  [L91–L97] status=200
          └─ sends_request GetWorkpaperRecordTemplateSectionsQuery [L94]
        └─ [web] GET /api/connections/xero/creditors  (Workpapers.Next.API.Controllers.Connections.XeroController.GetCreditors)  [L70–L76] status=200
        └─ [web] GET /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Get)  [L111–L122] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ClientDto [L116]
            └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
          └─ uses_client ClientRepository [L116]
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L118]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L118]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ sends_request CanIAccessClientQuery -> CanIAccessClientQueryHandler [L114]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessClientQueryHandler.Handle [L41–L104]
              └─ uses_service IControlledRepository<Client> (Scoped (inferred))
                └─ method ReadQuery [L85]
                  └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L83]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
                    └─ uses_service IRequestProcessor (InstancePerDependency)
                      └─ method GetCurrentSettings [L45]
                        └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings [L7-L35]
                    └─ uses_service TenantService
                      └─ method GetCurrentSettings [L34]
                        └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings [L26-L139]
                          └─ uses_service IRequestProcessor (InstancePerDependency)
                            └─ method GetCatalogByDataverseId [L111]
                              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
                          └─ uses_service Tenant
                            └─ method AssignCurrentTenantId [L80]
                              └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
                    └─ uses_cache IDistributedCache.SetStringAsync [write] [L108]
                    └─ uses_cache IDistributedCache.GetStringAsync [read] [L98]
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L71]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L71]
                  └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant (see previous expansion)
              └─ uses_service IRequestInfoService (AddScoped)
                └─ method IsValidServiceAccountRequest [L64]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L98]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L73]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L71]
        └─ [web] GET /workpapers/v1/sources/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.GetSourcesDetailedQuery)  [L75–L81] status=200
          └─ calls SourceRepository.ReadQuery [L78]
          └─ query Source [L78]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L78]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/stats/licensesummary  (Workpapers.Next.API.Controllers.StatsController.LicenseSummary)  [L44–L55] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetLicenseSummaryQuery -> GetLicenseSummaryQueryHandler [L52]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetLicenseSummaryQueryHandler.Handle [L40–L156]
              └─ uses_service UnitOfWork
                └─ method Table [L72]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=LicenseSummary) [L53]
        └─ [web] GET /workpapers/v1/matters/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersMinimalQuery)  [L71–L78] status=200
          └─ calls MatterRepository.ReadQuery [L74]
          └─ query Matter [L74]
            └─ reads_from Matters
        └─ [web] GET /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetById)  [L65–L73] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobFilterSetDto [L68]
            └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetDto) [L344]
          └─ calls JobFilterSetRepository.ReadQuery [L68]
          └─ query JobFilterSet [L68]
            └─ reads_from JobFilterSets
        └─ [web] GET /api/useroffices/{id}  (Workpapers.Next.API.Controllers.UserOfficesController.Get)  [L48–L55] status=200
          └─ maps_to UserOfficeDto [L51]
          └─ uses_service UnitOfWork
            └─ method Table [L51]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/tenants/subscriptions  (Dataverse.Api.Controllers.UI.TenantController.GetSubscriptions)  [L58–L68] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to SubscriptionLightDto [L62]
          └─ calls TenantRepository.ReadTable [L62]
          └─ query Tenant [L62]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L62]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L61]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /api/template-standard-accounts/sets  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccountSets)  [L76–L85] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to TemplateStandardAccountSetDto [L80]
            └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccountSet->TemplateStandardAccountSetDto) [L672]
          └─ calls TemplateStandardAccountSetRepository.ReadQuery [L80]
          └─ query TemplateStandardAccountSet [L80]
            └─ reads_from TemplateStandardAccountSets
        └─ [web] GET /workflow/v1/tasks/  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.MinimalQuery)  [L67–L77] status=200
          └─ calls TaskRepository.ReadQuery [L71]
          └─ query WorkflowTask [L71]
            └─ reads_from DVS_Tasks
        └─ [web] GET /api/ui/users/profile  (Dataverse.Api.Controllers.UI.Core.UsersController.Profile)  [L87–L93] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetUserProfileQuery -> GetUserProfileQueryHandler [L90]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserProfileQueryHandler.Handle [L34–L129]
              └─ calls TenantRepository.ReadTable [L79]
              └─ calls UserRepository.ReadQuery [L74]
              └─ maps_to NotificationDto [L120]
                └─ automapper.registration DataverseMappingProfile (Notification->NotificationDto) [L116]
              └─ maps_to IntegrationSettingsDto [L104]
                └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
              └─ maps_to FirmSettingsDto [L91]
                └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
              └─ maps_to UserProfileDto [L74]
                └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L93]
              └─ maps_to TenantLightDto [L118]
              └─ maps_to TenantDto [L112]
              └─ uses_service IControlledRepository<Notification> (Scoped (inferred))
                └─ method ReadQuery [L120]
                  └─ implementation Dataverse.Data.Repository.Users.NotificationRepository.ReadQuery
              └─ uses_service IControlledRepository<IntegrationSettings> (Scoped (inferred))
                └─ method ReadQuery [L104]
                  └─ implementation Dataverse.Data.Repository.Firm.IntegrationSettingsRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentWorkpapersSettingsAsync [L101]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentWorkpapersSettingsAsync [L18-L97]
                    └─ uses_client WorkpapersClient [L78]
                    └─ uses_service WorkpapersClient
                      └─ method GetCurrentWorkpapersSettingsAsync [L78]
                        └─ ... (no implementation details available)
              └─ uses_service FirmFeatureFlagService
                └─ method GetAvailableFeaturesForFirm [L96]
                  └─ implementation Dataverse.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L91]
                    └─ uses_service IControlledRepository<FirmFeatureFlag> (Scoped (inferred))
                      └─ method IsFirmFlagSet [L88]
                        └─ implementation Dataverse.Data.Repository.Firm.FirmFeatureFlagRepository.IsFirmFlagSet
                    └─ uses_service FirmSettingsService
                      └─ method IsFirmPartOfControlledBeta [L76]
                        └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.IsFirmPartOfControlledBeta [L18-L97]
                          └─ uses_client WorkpapersClient [L78]
                          └─ uses_service WorkpapersClient
                            └─ method GetCurrentWorkpapersSettingsAsync [L78]
                              └─ ... (no implementation details available)
                          └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
                            └─ method GetCurrentSettingsAsync [L52]
                              └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
                          └─ uses_service TenantService
                            └─ method GetCurrentSettingsAsync [L44]
                              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync (see previous expansion)
                    └─ uses_service FeatureFlagService
                      └─ method CanIUseFeature [L62]
                        └─ implementation Dataverse.ApplicationService.Features.FeatureFlags.FeatureFlagService.CanIUseFeature [L10-L34]
                          └─ uses_service IControlledRepository<FeatureFlag> (Scoped (inferred))
                            └─ method GetFeature [L30]
                              └─ implementation Dataverse.Data.Repository.Firm.FeatureFlagRepository.GetFeature
                          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L25]
                    └─ uses_service TenantService
                      └─ method CanIUseFeature [L59]
                        └─ implementation Dataverse.Services.Features.Tenants.TenantService.CanIUseFeature [L6-L27]
                          └─ uses_service TenantIdentificationService
                            └─ method GetCurrentTenant [L20]
                              └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
                    └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L83]
              └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
                └─ method ReadQuery [L91]
                  └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.ReadQuery
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L77]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L73]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/users/profile  (Cirrus.Api.Controllers.Firm.UsersController.Profile)  [L59–L76] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to FirmLightDto [L73]
          └─ maps_to ReportSettingsLightDto [L68]
            └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
          └─ maps_to UserProfileDto [L62]
            └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
          └─ calls ReportSettingsRepository.ReadQuery [L68]
          └─ calls UserRepository.ReadQuery [L62]
          └─ query ReportSettings [L68]
            └─ reads_from ReportSettings
          └─ query User [L62]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method GetUserId [L64]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L73]
        └─ [web] GET /api/templates/for-workpaper-record-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.GetForWorkpaperRecordTemplate)  [L50–L61] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to TemplateDto [L54]
          └─ uses_service UnitOfWork
            └─ method Table [L54]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/source-accounts/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForBinder)  [L89–L107] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L94]
          └─ query Binder [L94]
            └─ reads_from Binders
          └─ sends_request GetSourceAccountsQuery -> GetSourceAccountsQueryHandler [L106]
          └─ sends_request GetSourceAccountsLightQuery [L105]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L92]
        └─ [web] GET /api/imanage  (DataGet.Api.Controllers.Integrations.IManageController.GetApiInfo)  [L290–L296] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageApiInformationQuery -> GetIManageApiInformationQueryHandler [L292]
        └─ [web] GET /core/v1/offices/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditQuery)  [L112–L117] status=200
          └─ maps_to EntityAuditDto [L115]
          └─ calls OfficeRepository.ReadQuery [L115]
          └─ query Office [L115]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L115]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.GetAll)  [L34–L46] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CabinetDto [L39]
            └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L398]
          └─ calls CabinetRepository.ReadQuery [L39]
          └─ query Cabinet [L39]
            └─ reads_from Cabinets
        └─ [web] GET /api/stats/usage/template  (Workpapers.Next.API.Controllers.StatsController.GetRecordTemplatesUsage)  [L87–L99] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetRecordTemplatesUsageQuery -> GetRecordTemplatesUsageQueryHandler [L96]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetRecordTemplatesUsageQueryHandler.Handle [L65–L237]
              └─ maps_to WorkpaperRecordTemplateUltraLightDto [L133]
                └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
              └─ uses_client KeenClient [L154]
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L178]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L172]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service KeenQueryBuilder
                └─ method Build [L155]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L154]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L133]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TemplateUsage-period{*}-firmid{*}) [L102]
        └─ [web] GET /api/ui/templates/  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.FindTemplates)  [L32–L51] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/ui/sync-monitor/summary  (Dataverse.Api.Controllers.UI.SyncMonitorController.GetSummary)  [L63–L87] status=200 [auth=Authentication.AdminPolicy]
          └─ calls SyncConfigurationRepository.ReadQuery [L68]
          └─ calls DataverseEntityFailureLogRepository.ReadQuery [L66]
          └─ query SyncConfiguration [L68]
            └─ reads_from SyncConfigurations
          └─ query DataverseEntityFailureLog [L66]
            └─ reads_from DataverseEntityFailureLogs
        └─ [web] GET /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Get)  [L31–L37] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to ReportSettingsDto [L34]
            └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
          └─ calls ReportSettingsRepository.ReadQuery [L34]
          └─ query ReportSettings [L34]
            └─ reads_from ReportSettings
        └─ [web] GET /api/ui/documents/templates/  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.FindTemplates)  [L53–L87] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindDocumentTemplatesQuery -> FindDocumentTemplatesQueryHandler [L69]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentTemplatesQueryHandler.Handle [L57–L235]
              └─ uses_service UserService
                └─ method GetUserAsync [L80]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
              └─ uses_service IControlledRepository<DocumentTemplate> (Scoped (inferred))
                └─ method ReadQuery [L79]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentTemplateRepository.ReadQuery
        └─ [web] GET /api/super/workpapers/{tenantId:Guid}/tenant-creation-info  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetTenantCreationInfo)  [L53–L58] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetTenantCreateInfoQuery -> GetTenantCreateInfoQueryHandler [L57]
            └─ handled_by Dataverse.ApplicationService.Queries.Workpapers.GetTenantCreateInfoQueryHandler.Handle [L38–L81]
              └─ calls TenantRepository.ReadTable [L60]
              └─ uses_client WorkpapersClient [L73]
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L79]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_service WorkpapersClient
                └─ method Get [L73]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/super/sync-monitor/failures  (Dataverse.Api.Controllers.Super.SyncMonitorController.Search)  [L57–L82] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls DataverseEntityFailureLogRepository.ReadQuery [L67]
          └─ query DataverseEntityFailureLog [L67]
            └─ reads_from DataverseEntityFailureLogs
        └─ [web] GET /api/clients/  (Workpapers.Next.API.Controllers.ClientsController.Search)  [L45–L66] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetClientsQuery -> GetClientsQueryHandler [L61]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Clients.GetClientsQueryHandler.Handle [L70–L165]
              └─ calls ClientRepository.ReadQuery [L90]
              └─ uses_client ClientRepository [L90]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L88]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L87]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/connections/xero/files  (Workpapers.Next.API.Controllers.Connections.XeroController.GetAccountingFiles)  [L30–L36] status=200
        └─ [web] GET /api/sources/{type}/fixed-assets-workpaper-report  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetFixedAssetsReport)  [L449–L453] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetFixedAssetReportQuery -> GetFixedAssetReportQueryHandler [L452]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFixedAssetReportQueryHandler.Handle [L32–L126]
              └─ calls SourceAccountRepository.ReadQuery [L98]
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L81]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service ConnectionApiService
                └─ method GetApiMethods [L72]
                  └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L58]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /workflow/v1/deliverables/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditQuery)  [L118–L123] status=200
          └─ maps_to EntityAuditDto [L121]
          └─ calls DeliverableRepository.ReadQuery [L121]
          └─ query Deliverable [L121]
            └─ reads_from Deliverables
        └─ [web] GET /api/companies-house-gateway/submission-status  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetSubmissionStatusAsync)  [L69–L73] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetSubmissionStatusQuery -> GetSubmissionStatusQueryHandler [L72]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetSubmissionStatusQueryHandler.Handle [L43–L71]
              └─ maps_to SubmissionStatusResponseDto [L69]
              └─ uses_client CompaniesHouseInputGatewayClient [L65]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L65]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L64]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/users/intercom-profile  (Cirrus.Api.Controllers.Firm.UsersController.IntercomUserProfile)  [L85–L90] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /core/v1/users/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditQuery)  [L129–L134] status=200
          └─ maps_to EntityAuditDto [L132]
          └─ calls UserRepository.ReadQuery [L132]
          └─ query User [L132]
          └─ uses_service UserRepository
            └─ method ReadQuery [L132]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/teams  (Workpapers.Next.API.Controllers.TeamController.CheckAuthorisation)  [L172–L180] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L174]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /core/v1/teams/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditQuery)  [L111–L116] status=200
          └─ maps_to EntityAuditDto [L114]
          └─ calls TeamRepository.ReadQuery [L114]
          └─ query Team [L114]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L114]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/stats/quarterlysummary  (Workpapers.Next.API.Controllers.StatsController.QuarterlySummary)  [L202–L213] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=QuarterlySummary) [L205]
          └─ sends_request GetQuarterlySummaryQuery -> GetQuarterlySummaryQueryHandler [L209]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetQuarterlySummaryQueryHandler.Handle [L25–L155]
              └─ uses_client KeenClient [L82]
              └─ uses_service KeenQueryBuilder
                └─ method Build [L83]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L82]
                  └─ ... (no implementation details available)
              └─ uses_service UnitOfWork
                └─ method Table [L45]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/connections/bgl360/funds  (Workpapers.Next.API.Controllers.Connections.Bgl360Controller.GetFunds)  [L25–L31] status=200
        └─ [web] GET /api/companies-house/search/advanced-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAdvanced)  [L93–L107] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchAdvancedQuery -> SearchAdvancedQueryHandler [L104]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAdvancedQueryHandler.Handle [L40–L50]
        └─ [web] GET /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAll)  [L65–L69] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetSyncConfigurationsQuery -> GetSyncConfigurationsQueryHandler [L68]
            └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationsQueryHandler.Handle [L32–L97]
              └─ maps_to SyncConfigurationLightDto [L60]
              └─ maps_to SyncConfigurationDto [L56]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L69]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
                └─ method ReadQuery [L49]
                  └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.ReadQuery
        └─ [web] GET /api/companies-house/company/{companyNumber}/registered-office-address  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisteredOfficeAddress)  [L109–L117] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyRegisteredOfficeAddressQuery -> GetCompanyRegisteredOfficeAddressQueryHandler [L114]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegisteredOfficeAddressQueryHandler.Handle [L15–L25]
        └─ [web] GET /api/accounting/reports/published/template/{templateId:guid}/current  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Current)  [L80–L91] status=200 [auth=user]
          └─ maps_to PublishedReportBatchDto [L90]
          └─ calls PublishedReportBatchRepository.ReadQuery [L83]
          └─ query PublishedReportBatch [L83]
            └─ reads_from PublishedReportBatches
          └─ uses_service IRequestProcessor (InstancePerDependency)
            └─ method ProcessAsync [L88]
              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L88]
        └─ [web] GET /api/stats/firmusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.FirmUsageSummary)  [L57–L69] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetFirmUsageSummaryQuery -> GetFirmUsageSummaryQueryHandler [L66]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmUsageSummaryQueryHandler.Handle [L47–L155]
              └─ uses_client KeenClient [L93]
              └─ uses_service KeenQueryBuilder
                └─ method Build [L94]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L93]
                  └─ ... (no implementation details available)
              └─ uses_service UnitOfWork
                └─ method Table [L88]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=FirmUsage-period{*}-firmid{*}) [L70]
        └─ [web] GET /api/accounting/ledger/journals  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetJournals)  [L61–L74] status=200 [auth=user]
          └─ maps_to JournalDto [L67]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L65]
        └─ [web] GET /api/source-accounts/for-report  (Workpapers.Next.API.Controllers.SourceAccountsController.GetForReport)  [L109–L114] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetSourceAccountsForReportQuery -> GetSourceAccountsForReportQueryHandler [L113]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsForReportQueryHandler.Handle [L38–L125]
              └─ calls SourceAccountRepository.ReadQuery [L97]
              └─ maps_to FirmTolerancesDto [L117]
                └─ automapper.registration WorkpapersMappingProfile (Firm->FirmTolerancesDto) [L177]
              └─ maps_to BinderAccountRecordDto [L97]
              └─ maps_to SourceAccountForReportDto [L75]
                └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountForReportDto) [L606]
              └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
                └─ method ReadQuery [L117]
                  └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.ReadQuery
              └─ uses_service UserService
                └─ method GetFirmId [L116]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L67]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L65]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/source-accounts/{id:guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Get)  [L54–L58] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetSourceAccountByIdQuery [L57]
        └─ [web] GET /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderTypeRecordTemplates)  [L94–L107] status=200
          └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L100]
          └─ query BinderTypeRecordTemplateSet [L100]
            └─ reads_from BinderTypeRecordTemplateSets
        └─ [web] GET /workflow/v1/deliverables/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditsQuery)  [L104–L110] status=200
          └─ maps_to EntityAuditDto [L107]
          └─ calls DeliverableRepository.ReadQuery [L107]
          └─ query Deliverable [L107]
            └─ reads_from Deliverables
        └─ [web] GET /api/reportance/cirrus  (Workpapers.Next.API.Controllers.Reportance.CirrusController.GetFirmId)  [L101–L112] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L103]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/products/forsubscriptionunit/{subscriptionUnit}  (Workpapers.Next.API.Controllers.ProductsController.GetAllForSubscriptionUnit)  [L100–L108] status=200
          └─ maps_to ProductLightDto [L103]
          └─ uses_service UnitOfWork
            └─ method Table [L103]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/fyi/cabinets/{cabinetId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetCabinet)  [L55–L61] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetCabinetAsync [L58]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCabinetAsync [L19-L291]
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/profile  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentProfile)  [L270–L278] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageDocumentProfileQuery -> GetIManageDocumentProfileQueryHandler [L277]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentProfileQueryHandler.Handle [L24–L40]
              └─ uses_service IManageService
                └─ method GetDocumentProfile [L35]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocumentProfile [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/sources/export/{id:guid}/accounts  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetAccounts)  [L68–L73] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetExportSourceAccountsQuery -> GetExportSourceAccountsQueryHandler [L71]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceAccountsQueryHandler.Handle [L29–L68]
              └─ uses_client DataGetClient [L46]
                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ sends_request GetAccountsQuery -> GetAccountsQueryHandler [L48]
                        └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
                          └─ uses_client DataGetClient [L40]
                            └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                              └─ remote_endpoint_expansion_suppressed (see previous expansion)
                          └─ uses_service DataGetClient
                            └─ method GetAccountsAsync [L40]
                              └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
                                └─ calls GetAuthorisationUrl [L45]
                                └─ calls Disconnect [L55]
                                └─ calls DisconnectFile [L65]
                                └─ calls GetAccountingFiles [L74]
                                └─ calls TestConnection [L84]
                                └─ calls GetSourceDivisions [L95]
                                └─ calls GetAccounts [L106]
                                └─ calls GetMovements [L130]
                                └─ calls GetTransactions [L151]
                                └─ calls PostJournal [L161]
                                └─ calls PostAccount [L171]
                                └─ calls DeleteJournal [L182]
                                └─ calls GetCreditors [L194]
                                └─ calls GetDebtors [L206]
                                └─ calls GetWages [L218]
                                └─ calls StoreExistingToken [L228]
                                └─ calls StoreExistingFileTokens [L238]
                                └─ calls RequestLodgementAsync [L244]
                          └─ uses_service DatagetFileIdService
                            └─ method GetFileIdFromSource [L38]
                              └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetAccountsAsync [L46]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetAccountsAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
                └─ method ReadQuery [L42]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
        └─ [web] GET /api/reportance/cirrus/starters  (Workpapers.Next.API.Controllers.Reportance.CirrusController.GetStarters)  [L58–L69] status=200 [auth=AuthorizationPolicies.M2M]
          └─ maps_to StarterDto [L68]
          └─ sends_request AllStartersForProductQuery -> AllStartersForProductQueryHandler [L65]
            └─ handled_by Workpapers.Next.Data.Queries.Templates.Starters.AllStartersForProductQueryHandler.Handle [L17–L63]
              └─ uses_service UnitOfWork
                └─ method Table [L40]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service UserService
                └─ method IsSuperUser [L35]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L35]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/ui/imanage/libraries  (Dataverse.Api.Controllers.UI.IManageController.GetLibraries)  [L144–L164] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L147]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L147]
          └─ query IntegrationSettings [L147]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetLibraries [L153]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetLibraries [L19-L225]
        └─ [web] GET /api/stats/binder-type-usage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.FirmBinderTypeUsage)  [L130–L143] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetBinderTypeUsageQuery -> GetBinderTypeUsageQueryHandler [L140]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetBinderTypeUsageQueryHandler.Handle [L36–L90]
              └─ uses_service IControlledRepository<BinderType> (Scoped (inferred))
                └─ method ReadQuery [L70]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTypeRepository.ReadQuery
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
        └─ [web] GET /api/internal/workpapers/binders/{binderId:Guid}/tax-info  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.ReadTaxDataFromDocument)  [L41–L45] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ sends_request GetTaxDataFromBinderQuery -> GetTaxDataFromBinderQueryHandler [L44]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTaxDataFromBinderQueryHandler.Handle [L40–L360]
              └─ uses_service DataverseService
                └─ method GetStandardQueryParams [L355]
                  └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
                    └─ uses_service TenantIdentificationService
                      └─ method GetStandardQueryParams [L70]
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams (see previous expansion)
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L63]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L61]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
        └─ [web] GET /api/export/binders/{binderId:guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.BinderExportController.GetTrialBalanceExportData)  [L39–L63] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetTrialBalanceExportDataQuery -> GetTrialBalanceExportDataQueryHandler [L60]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTrialBalanceExportDataQueryHandler.Handle [L32–L160]
              └─ calls SourceAccountRepository.ReadQuery [L141]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L61]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L50]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
        └─ [web] GET /api/accounting/reports/labels/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.GetAll)  [L42–L49] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ReportLabelWithAccountDto [L45]
            └─ automapper.registration CirrusMappingProfile (ReportLabel->ReportLabelWithAccountDto) [L650]
          └─ calls ReportLabelRepository.ReadQuery [L45]
          └─ query ReportLabel [L45]
            └─ reads_from ReportLabels
        └─ [web] GET /api/binder-templates/  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Search)  [L75–L80] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetBinderTemplatesQuery -> GetBinderTemplatesQueryHandler [L79]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.BinderTemplates.GetBinderTemplatesQueryHandler.Handle [L47–L136]
              └─ uses_service IControlledRepository<ExcludedBinderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L132]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedBinderTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L98]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
              └─ uses_service UserService
                └─ method IsSuperUser [L96]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
        └─ [web] GET /api/users/fromcentral/{id}  (Workpapers.Next.API.Controllers.UsersController.GetByCentralId)  [L119–L129] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to UserDto (var user) [L123]
          └─ calls UserRepository.ReadQuery [L123]
          └─ query User [L123]
          └─ uses_service UserRepository
            └─ method ReadQuery [L123]
              └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/assets/asset-groups/{id}  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.Get)  [L41–L47] status=200 [auth=user]
          └─ maps_to AssetGroupDto [L44]
            └─ automapper.registration CirrusMappingProfile (AssetGroup->AssetGroupDto) [L874]
          └─ calls AssetGroupRepository.ReadQuery [L44]
          └─ query AssetGroup [L44]
            └─ reads_from AssetGroups
        └─ [web] GET /api/accounting/ledger/import-runs/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.Get)  [L50–L62] status=200 [auth=user]
          └─ maps_to JournalLightDto [L58]
          └─ maps_to ImportRunDto [L53]
            └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunDto) [L517]
          └─ calls ImportRunRepository.ReadQuery [L53]
          └─ query ImportRun [L53]
            └─ reads_from ImportRuns
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L55]
        └─ [web] GET /api/files/download  (Workpapers.Next.API.Controllers.LegacyController.Download)  [L69–L87] status=200
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L84]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UserService
            └─ method GetUser [L76]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L72]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L84]
          └─ sends_request GetTemplateForDateTimeQuery -> GetTemplateForDateTimeQueryHandler [L76]
            └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L23]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/ui/sync-configuration/type/{type}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAllByType)  [L65–L77] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to SyncConfigurationUltraLightDto [L70]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L265]
          └─ calls SyncConfigurationRepository.ReadQuery [L70]
          └─ query SyncConfiguration [L70]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/workpapers/byteam  (Workpapers.Next.API.Controllers.WorkpapersController.GetForTeam)  [L69–L73] status=200
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/clients  (DataGet.Api.Controllers.Integrations.IManageController.GetClients)  [L186–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageClientsQuery -> GetIManageClientsQueryHandler [L194]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageClientsQueryHandler.Handle [L24–L42]
              └─ uses_service IManageService
                └─ method GetClients [L35]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetClients [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /workflow/v1/jobs/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.FullQuery)  [L89–L97] status=200
          └─ calls JobRepository.ReadQuery [L93]
          └─ query Job [L93]
            └─ reads_from Jobs
        └─ [web] GET /api/accounting/reports/notes/reporting-suites/unselected-policy-variants  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetUnselectedPolicyVariants)  [L83–L90] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetPoliciesWithVariantsQuery [L88]
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/documents  (DataGet.Api.Controllers.Integrations.IManageController.GetDocuments)  [L237–L246] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageDocumentsQuery -> GetIManageDocumentsQueryHandler [L245]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentsQueryHandler.Handle [L26–L60]
              └─ uses_service IntegrationConfigService
                └─ method GetApiIntegrationConfig [L53]
                  └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetApiIntegrationConfig [L8-L37]
                    └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
                      └─ method GetApiIntegrationConfig [L19]
                        └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.GetApiIntegrationConfig [L5-L35]
              └─ uses_service IManageService
                └─ method GetDocuments [L41]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocuments [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/sources/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetForClient)  [L92–L102] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to SourceDto [L95]
            └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
            └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
          └─ calls SourceRepository.ReadQuery [L95]
          └─ query Source [L95]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L95]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/assets/pooling/manage  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.GetPoolManageDetails)  [L29–L40] status=200 [auth=user]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L32]
        └─ [web] GET /api/internal/Propagator/request-audit  (Dataverse.Api.Controllers.Internal.PropagatorController.RequestAudit)  [L88–L98] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls TenantRepository.ReadTable [L92]
          └─ query Tenant [L92]
            └─ reads_from Tenants
          └─ uses_service MockMessagingService
            └─ method RequestAudit [L97]
              └─ ... (no implementation details available)
          └─ uses_service TenantRepository
            └─ method ReadTable [L92]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/starters/byproduct/{ProductId:Guid}/active  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProductActive)  [L72–L79] status=200
          └─ maps_to StarterDto [L78]
          └─ uses_service UserService
            └─ method GetUser [L75]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ sends_request AllStartersForProductQuery -> AllStartersForProductQueryHandler [L75]
        └─ [web] GET /api/fyi/groups/{groupId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetGroup)  [L208–L215] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetGroupQuery -> GetGroupQueryHandler [L211]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetGroup [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetGroup [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/ato/gov-link/activity-statements  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshActivityStatements)  [L118–L125] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetActivityStatementsQuery -> GetActivityStatementsQueryHandler [L122]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementsQueryHandler.Handle [L32–L66]
              └─ uses_client DataGetClient [L60]
                └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementsAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ uses_service AtoService
                        └─ method GetActivityStatements [L38]
                          └─ implementation GovLink.Application.Services.AtoService.GetActivityStatements [L12-L145]
              └─ uses_service DataGetClient
                └─ method GetActivityStatementsAsync [L60]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementsAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L52]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
            └─ handled_by GovLink.Application.Queries.ActivityStatements.GetActivityStatementQueryHandler.Handle [L90–L198]
              └─ uses_client IAtoClient [L110]
              └─ uses_service IAtoClient (AddScoped)
                └─ method RequestAsync [L110]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/ledger/reports/{datasetId}/sourceaccounts  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetLedgerForSourceAccounts)  [L76–L95] status=200 [auth=user]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L92]
        └─ [web] GET /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Search)  [L59–L78] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetMatterTextTemplatesQuery -> GetMatterTextTemplatesQueryHandler [L73]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.Matters.GetMatterTextTemplatesQueryHandler.Handle [L124–L217]
              └─ maps_to WorkpaperRecordTemplateLightDto [L155]
                └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateLightDto) [L221]
              └─ uses_service IControlledRepository<MatterTextTemplate> (Scoped (inferred))
                └─ method ReadQuery [L174]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterTextTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L155]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
        └─ [web] GET /api/ui/cloud-capcha/test-connection  (Dataverse.Api.Controllers.UI.CloudCapchaController.TestConnection)  [L45–L50] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
          └─ uses_service IDataGetCloudCapchaService (AddTransient)
            └─ method TestConnection [L49]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.TestConnection [L13-L49]
        └─ [web] GET /health/  (Workpapers.Next.API.Controllers.HealthController.Alive)  [L9–L14] status=200 [AllowAnonymous]
        └─ [web] GET /api/super/users/all  (Dataverse.Api.Controllers.Super.Core.UsersController.Search)  [L78–L89] status=200 [auth=Authentication.MasterPolicy]
          └─ sends_request FindCentralUsersQuery -> FindCentralUsersQueryHandler [L88]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindCentralUsersQueryHandler.Handle [L48–L116]
              └─ calls TenantRepository.ReadTable [L69]
              └─ uses_service UserService
                └─ method GetIdentityId [L73]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]
                    └─ uses_service RequestInfoService
                      └─ method GetIdentityId [L160]
                        └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
        └─ [web] GET /api/super/tenant-reports/tenant-report/{tenantId:guid}  (Dataverse.Api.Controllers.Super.TenantReportsController.GetTenantReport)  [L31–L46] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request TenantReportBaseQuery [L44]
        └─ [web] GET /api/sources/for-active-ledger  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetSourceForActiveLedger)  [L486–L491] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L489]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/workpaper-records/for-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordInfoForReport)  [L146–L151] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetWorkpaperRecordInfoForReportQuery -> GetWorkpaperRecordInfoForReportQueryHandler [L150]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordInfoForReportQueryHandler.Handle [L32–L100]
              └─ maps_to WorkpaperRecordForReportDto [L55]
                └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordForReportDto) [L539]
              └─ uses_service MatterCountQueryProcessor
                └─ method ProcessMatterCounts [L72]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L53]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/ui/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.Get)  [L59–L63] status=200 [auth=Authentication.AdminPolicy]
          └─ sends_request GetSyncConfigurationQuery -> GetSyncConfigurationQueryHandler [L62]
            └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationQueryHandler.Handle [L34–L90]
              └─ maps_to SyncConfigurationDto [L54]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L65]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
                └─ method ReadQuery [L49]
                  └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.ReadQuery
        └─ [web] GET /api/fyi/access-info  (DataGet.Api.Controllers.Integrations.FyiController.GetAccessInfo)  [L255–L268] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service UserTokenService
            └─ method GetTokenAsync [L260]
              └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync (see previous expansion)
        └─ [web] GET /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.GetSubscription)  [L38–L48] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to SubscriptionDto [L41]
          └─ uses_service UnitOfWork
            └─ method Table [L41]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/central/firms/{firmId}/owners  (Cirrus.Api.Controllers.Central.FirmController.GetOwners)  [L74–L83] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to FirmUserDto [L77]
          └─ calls CentralRepository.ReadTable [L77]
          └─ uses_service CentralRepository
            └─ method ReadTable [L77]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetSubscription)  [L60–L73] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L65]
          └─ calls TenantRepository.ReadTable [L65]
          └─ query Tenant [L65]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L65]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/ui/workflow/jobs/default-name  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobDefaultName)  [L95–L100] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request DefaultJobNameQuery -> DefaultJobNameQueryHandler [L98]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultJobNameQueryHandler.Handle [L32–L73]
              └─ maps_to JobTypeDto [L51]
              └─ uses_service JobParamsService
                └─ method GetClient [L50]
                  └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient [L24-L215]
                    └─ uses_client ClientRepository [L137]
                    └─ uses_service FirmSettingsService
                      └─ method GetClient [L142]
                        └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetClient [L18-L97]
                          └─ uses_client WorkpapersClient [L78]
                          └─ uses_service WorkpapersClient
                            └─ method GetCurrentWorkpapersSettingsAsync [L78]
                              └─ ... (no implementation details available)
                          └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
                            └─ method GetCurrentSettingsAsync [L52]
                              └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
                          └─ uses_service TenantService
                            └─ method GetCurrentSettingsAsync [L44]
                              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync (see previous expansion)
                    └─ uses_service RequestInfoService
                      └─ method GetClient [L136]
                        └─ implementation Dataverse.Services.Features.RequestInfoService.GetClient [L11-L92]
                          └─ uses_service RequestInfo
                            └─ method IsValidServiceAccountRequest [L82]
                              └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                              └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                              └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                          └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                    └─ uses_service List<ClientDto>
                      └─ method GetClient [L133]
                        └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.GetClient [L60-L77]
                          └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                          └─ uses_service IRequestProcessor (InstancePerDependency)
                            └─ method ProcessAsync [L70]
                              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
                          └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                            └─ method ReadQuery [L66]
                              └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                          └─ query PublishedReportBatch [L66]
                          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
                          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
        └─ [web] GET /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Get)  [L61–L69] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to BinderFieldDto [L65]
            └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
          └─ calls BinderFieldRepository.ReadQuery [L65]
          └─ query BinderField [L65]
            └─ reads_from BinderFields
        └─ [web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetIncomeTaxProfileComparison [L49]
              └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxProfileComparison [L12-L145]
        └─ [web] GET /api/matching-rule-sets/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.GetAll)  [L48–L57] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to MatchingRuleSetDto [L51]
            └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
          └─ calls MatchingRuleSetRepository.ReadQuery [L51]
          └─ query MatchingRuleSet [L51]
            └─ reads_from MatchingRuleSets
          └─ uses_service MatchingRuleSetRepository
            └─ method ReadQuery [L51]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.ReadQuery (see previous expansion)
        └─ [web] GET /core/v1/teams/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.Get)  [L51–L56] status=200
          └─ maps_to TeamDto [L54]
            └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
            └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
          └─ calls TeamRepository.ReadQuery [L54]
          └─ query Team [L54]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L54]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/named-ranges  (Workpapers.Next.API.Controllers.Templates.NamedRangesController.Get)  [L30–L47] status=200
          └─ calls NamedRangeRepository.ReadQuery [L33]
          └─ query NamedRange [L33]
            └─ reads_from NamedRanges
        └─ [web] GET /api/ui/offices/code/{code}  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetByCode)  [L92–L100] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to OfficeDto [L95]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
            └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
          └─ calls OfficeRepository.ReadQuery [L95]
          └─ query Office [L95]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L95]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /workflow/v1/job-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditQuery)  [L116–L121] status=200
          └─ maps_to EntityAuditDto [L119]
          └─ calls JobTypeRepository.ReadQuery [L119]
          └─ query JobType [L119]
            └─ reads_from JobTypes
        └─ [web] GET /api/ui/workflow/job-types/ultra-light  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetUltraLight)  [L54–L64] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobTypeUltraLightDto [L57]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeUltraLightDto) [L319]
          └─ calls JobTypeRepository.ReadQuery [L57]
          └─ query JobType [L57]
            └─ reads_from JobTypes
        └─ [web] GET /api/ui/configurations/friction-map-refresh/{userEmail}  (Dataverse.Api.Controllers.UI.Configuration.ConfigurationController.IsUserAllowedForFrictionMapRefresh)  [L31–L35] status=200 [auth=Authentication.MasterPolicy]
        └─ [web] GET /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Get)  [L39–L49] status=200 [auth=user]
          └─ maps_to HeaderTemplateDto (var dto) [L46]
          └─ calls ReportContentRepository.LoadReadProperties [L45]
          └─ calls HeaderTemplateRepository.ReadQuery [L42]
          └─ query HeaderTemplate [L42]
            └─ reads_from ReportHeaderTemplates
          └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
            └─ method LoadReadProperties [L45]
              └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
          └─ sends_request PrepareImagesContentCommand [L47]
        └─ [web] GET /api/ui/documents/statuses/list  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAllLight)  [L44–L53] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentStatusLightDto [L47]
            └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusLightDto) [L413]
          └─ calls DocumentStatusRepository.ReadQuery [L47]
          └─ query DocumentStatus [L47]
            └─ reads_from DVS_DocumentStatuses
        └─ [web] GET /api/binder-document-defaults/  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Get)  [L50–L65] status=200
          └─ maps_to BinderDocumentDefaultsDto [L53]
            └─ automapper.registration WorkpapersMappingProfile (BinderDocumentDefaults->BinderDocumentDefaultsDto) [L897]
          └─ calls BinderDocumentDefaultsRepository.ReadQuery [L53]
          └─ query BinderDocumentDefaults [L53]
            └─ reads_from BinderDocumentDefaults
        └─ [web] GET /api/journals/for-binder-column/{binderColumnId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetForBinderColumn)  [L118–L137] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to JournalLightDto [L131]
          └─ calls JournalRepository.ReadQuery [L129]
          └─ calls BinderRepository.ReadQuery [L121]
          └─ query Journal [L129]
          └─ query Binder [L121]
            └─ reads_from Binders
          └─ uses_service JournalRepository
            └─ method ReadQuery [L129]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.JournalRepository.ReadQuery [L12-L72]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L125]
        └─ [web] GET /api/ui/fyi-elite/access-info  (Dataverse.Api.Controllers.UI.FyiEliteController.GetAccessInfo)  [L41–L48] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ uses_service IDatagetFyiEliteService (AddTransient)
            └─ method GetAccessInfo [L45]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.GetAccessInfo [L13-L53]
        └─ [web] GET /api/fyi/documents  (DataGet.Api.Controllers.Integrations.FyiController.GetDocuments)  [L89–L109] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentsQuery -> GetDocumentsQueryHandler [L105]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentsQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetDocuments [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetDocuments [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/ui/workflow/excel/download-master  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadTemplate)  [L39–L47] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L44]
        └─ [web] GET /api/connections/reportance/creditors  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetCreditors)  [L100–L106] status=200
        └─ [web] GET /api/internal/Propagator/user/{id:guid}  (Dataverse.Api.Controllers.Internal.PropagatorController.GetUser)  [L113–L117] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserWithLicensesDto [L116]
            └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
          └─ calls UserRepository.ReadQuery [L116]
          └─ query User [L116]
          └─ uses_service UserRepository
            └─ method ReadQuery [L116]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/binder-types/  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetAll)  [L48–L54] status=200
          └─ sends_request GetValidBinderTypesQuery -> GetValidBinderTypesQueryHandler [L51]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetValidBinderTypesQueryHandler.Handle [L31–L77]
              └─ maps_to BinderTypeLightDto [L69]
                └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeLightDto) [L762]
              └─ uses_service IControlledRepository<BinderType> (Scoped (inferred))
                └─ method ReadQuery [L69]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTypeRepository.ReadQuery
              └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L57]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<ExcludedBinderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedBinderTemplateRepository.ReadQuery
        └─ [web] GET /workpapers/v1/binders/{binderId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.Get)  [L49–L54] status=200
          └─ maps_to BinderDto [L52]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderDto) [L450]
            └─ automapper.registration ExternalApiMappingProfile (Binder->BinderDto) [L58]
          └─ calls BinderRepository.ReadQuery [L52]
          └─ query Binder [L52]
            └─ reads_from Binders
        └─ [web] GET /api/ui/documents/documents/external/{externalId}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentByExternalId)  [L431–L450] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentDto [L442]
            └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
          └─ calls DocumentRepository.ReadQuery [L434]
          └─ query Document [L434]
            └─ reads_from Documents
        └─ [web] GET /api/gov-link/individual-income-tax-returns/  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReport)  [L23–L32] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetIncomeTaxReport [L28]
              └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxReport [L12-L145]
        └─ [web] GET /api/tenant/databases/{databaseId}  (Dataverse.Api.Controllers.Tenants.DatabaseController.Get)  [L45–L52] status=200 [auth=master]
          └─ maps_to DatabaseDto [L48]
          └─ calls TenantRepository.ReadTable [L48]
          └─ query Tenant [L48]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L48]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/internal/task-templates/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.GetById)  [L45–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TaskTemplateDto [L48]
            └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
          └─ calls TaskTemplateRepository.ReadQuery [L48]
          └─ query TaskTemplate [L48]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/claims/{identityId}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetClaims)  [L54–L113] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ calls TenantRepository.ReadTable [L57]
          └─ query Tenant [L57]
            └─ reads_from Tenants
          └─ uses_service ITenantIdentificationService (AddScoped)
            └─ method AssignActiveTenant [L68]
              └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]
                └─ uses_service TenantDetails
                  └─ method AssignActiveTenant [L77]
                    └─ ... (no implementation details available)
                └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
          └─ uses_service TenantRepository
            └─ method ReadTable [L57]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
          └─ sends_request GetTenantsForUserQuery -> GetTenantsForUserQueryHandler [L64]
            └─ handled_by Dataverse.Tenants.Queries.GetTenantsForUserQueryHandler.Handle [L31–L55]
              └─ calls TenantRepository.ReadTable [L47]
              └─ maps_to TenantWithUserInfoDto [L47]
        └─ [web] GET /api/binder-status-requirements/  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Get)  [L33–L41] status=200
          └─ maps_to BinderStatusRequirementsDto [L36]
            └─ automapper.registration WorkpapersMappingProfile (BinderStatusRequirements->BinderStatusRequirementsDto) [L911]
          └─ calls BinderStatusRequirementsRepository.ReadQuery [L36]
          └─ query BinderStatusRequirements [L36]
            └─ reads_from BinderStatusRequirements
        └─ [web] GET /api/ui/cloud-capcha/access-info  (Dataverse.Api.Controllers.UI.CloudCapchaController.GetAccessInfo)  [L24–L29] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
          └─ uses_service IDataGetCloudCapchaService (AddTransient)
            └─ method GetAccessInfo [L28]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.GetAccessInfo [L13-L49]
        └─ [web] GET /api/companies-house/disqualified-officers/corporate/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCorporateDisqualification)  [L233–L241] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCorporateDisqualificationQuery -> GetCorporateDisqualificationQueryHandler [L238]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCorporateDisqualificationQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/ui/documents/documents/{id}/{fileExtension}/preview  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocument)  [L198–L205] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDocumentPreviewLink -> GetDocumentPreviewLinkHandler [L202]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkHandler.Handle [L34–L119]
              └─ maps_to SecureDocumentStoreDto [L107]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L114]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L98]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service DocumentServiceFactory
                └─ method GetDefaultColdStorageDocumentWithService [L90]
                  └─ implementation DocumentServiceFactory.GetDefaultColdStorageDocumentWithService
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method WriteQuery [L69]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
              └─ uses_service string[]
                └─ method Contains [L67]
                  └─ ... (no implementation details available)
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L201]
        └─ [web] GET /api/accounting/ledger/standard-accounts/recommendations  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForFile)  [L70–L91] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardAccountRecommendationDto [L80]
            └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountRecommendationDto) [L444]
          └─ calls StandardAccountRepository.ReadQuery [L80]
          └─ query StandardAccount [L80]
            └─ reads_from StandardAccounts
        └─ [web] GET /api/accounting/tax-forms/{formId}/balances/{datasetId}  (Cirrus.Api.Controllers.Accounting.Tax.TaxFormsController.GetFormBalances)  [L32–L37] status=200 [auth=user]
          └─ sends_request GetTaxLabelBalancesQuery -> GetTaxLabelBalancesQueryHandler [L36]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetTaxLabelBalancesQueryHandler.Handle [L25–L88]
              └─ uses_service GetTaxLabelBalancesByCurrentClassificationQuery
                └─ method Execute [L58]
                  └─ ... (no implementation details available)
              └─ uses_service GetTaxLabelBalancesByClassificationQuery
                └─ method Execute [L54]
                  └─ ... (no implementation details available)
              └─ uses_service GetTaxLabelBalancesByAccountTypeQuery
                └─ method Execute [L50]
                  └─ ... (no implementation details available)
              └─ uses_service GetTaxLabelsQuery
                └─ method Execute [L47]
                  └─ ... (no implementation details available)
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L35]
        └─ [web] GET /api/ui/fyi/documents  (Dataverse.Api.Controllers.UI.FyiController.GetDocuments)  [L79–L96] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetDocumentsAsync [L93]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentsAsync [L19-L291]
        └─ [web] GET /api/companies-house-gateway/charge-search  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetChargeSearchAsync)  [L58–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetChargeSearchQuery -> GetChargeSearchQueryHandler [L61]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetChargeSearchQueryHandler.Handle [L51–L80]
              └─ maps_to ChargesResponseDto [L78]
              └─ uses_client CompaniesHouseInputGatewayClient [L74]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L74]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L73]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/gov-link/client-accounts/summary  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionSummary)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetClientAccountSummary [L38]
              └─ implementation GovLink.Application.Services.AtoService.GetClientAccountSummary [L12-L145]
        └─ [web] GET /api/internal/users  (Dataverse.Api.Controllers.Internal.Core.UsersController.PopulateIntegrationAccessFlagsAsync)  [L205–L218] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method TestConnection [L211]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.TestConnection (see previous expansion)
        └─ [web] GET /api/ui/workflow/jobs/find  (Dataverse.Api.Controllers.UI.Workflow.JobController.FindJobs)  [L54–L70] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindJobsQuery -> FindJobsQueryHandler [L69]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindJobsQueryHandler.Handle [L178–L538]
              └─ calls JobRepository.ReadQuery [L269]
              └─ maps_to JobDto [L245]
                └─ converts_to JobExportDto [L312]
              └─ maps_to JobExportDto [L258]
              └─ maps_to WorkflowTaskDto [L251]
              └─ maps_to DeliverableDto [L249]
              └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
                └─ method ReadQuery [L322]
                  └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L270]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L270]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L217]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/firms/checkname  (Workpapers.Next.API.Controllers.Firms.FirmsController.CheckName)  [L114–L120] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L117]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /ledger/v1/files/{fileId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.Get)  [L67–L72] status=200
          └─ maps_to FileDto [L70]
            └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
            └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
          └─ calls FileRepository.ReadQuery [L70]
          └─ query File [L70]
            └─ reads_from Files
        └─ [web] GET /api/import-runs/for-binder  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.GetForBinder)  [L70–L90] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ImportRunLightDto [L82]
            └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunLightDto) [L816]
          └─ calls ImportRunRepository.ReadQuery [L82]
          └─ calls BinderRepository.ReadQuery [L73]
          └─ query ImportRun [L82]
            └─ reads_from ImportRuns
          └─ query Binder [L73]
            └─ reads_from Binders
          └─ uses_service ImportRunRepository
            └─ method ReadQuery [L82]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.ImportRunRepository.ReadQuery [L10-L51]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L77]
        └─ [web] GET /ledger/v1/standard-charts/{standardChartId:int}  (Cirrus.Api.External.Controllers.v1.Ledger.StandardChartsController.Get)  [L45–L51] status=200
          └─ maps_to StandardChartDto [L48]
            └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
            └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
          └─ calls StandardChartRepository.ReadQuery [L48]
          └─ query StandardChart [L48]
            └─ reads_from StandardCharts
        └─ [web] GET /api/ui/sync-configuration/  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAll)  [L53–L57] status=200 [auth=Authentication.AdminPolicy]
          └─ sends_request GetSyncConfigurationsQuery -> GetSyncConfigurationsQueryHandler [L56]
        └─ [web] GET /api/ui/imanage  (Dataverse.Api.Controllers.UI.IManageController.UploadFile)  [L314–L326] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L316]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L316]
          └─ query IntegrationSettings [L316]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method UploadFile [L322]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.UploadFile [L19-L225]
        └─ [web] GET /api/ato/profile-compare  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetProfileComparison)  [L110–L116] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetIndividualIncomeTaxReturnProfileCompareQuery -> GetIndividualIncomeTaxReturnProfileCompareQueryHandler [L113]
            └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L116–L134]
              └─ uses_client IAtoClient [L130]
              └─ uses_service IAtoClient (AddScoped)
                └─ method RequestAsync [L130]
                  └─ ... (no implementation details available)
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L33–L73]
              └─ uses_client DataGetClient [L60]
                └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetProfileCompareAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetProfileCompareAsync [L60]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetProfileCompareAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L52]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
          └─ returns ProfileCompareDto [L113]
        └─ [web] GET /core/v1/users/profile  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Profile)  [L54–L59] status=200
          └─ maps_to UserWithTenantIdDto [L57]
            └─ automapper.registration ExternalApiMappingProfile (User->UserWithTenantIdDto) [L66]
          └─ calls UserRepository.ReadQuery [L57]
          └─ query User [L57]
          └─ uses_service UserService
            └─ method GetUserId [L58]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UserRepository
            └─ method ReadQuery [L57]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/cabinets/default  (Dataverse.Api.Controllers.Internal.Documents.CabinetsController.GetDefault)  [L32–L42] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to CabinetDto [L35]
            └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L398]
          └─ calls CabinetRepository.ReadQuery [L35]
          └─ query Cabinet [L35]
            └─ reads_from Cabinets
        └─ [web] GET /api/ui/imanage/workspaces  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaces)  [L183–L198] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L186]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L186]
          └─ query IntegrationSettings [L186]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetWorkspacesAsync [L192]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetWorkspacesAsync [L19-L225]
        └─ [web] GET /api/ui/fyi/categories  (Dataverse.Api.Controllers.UI.FyiController.GetCategories)  [L63–L69] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetCategoriesAsync [L66]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCategoriesAsync [L19-L291]
        └─ [web] GET /api/accounting/reports/layout/master/{pageTypeId:int}/layout  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLayoutController.GetMasterPageLayoutOptions)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/matching-rules  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.ConfirmHasAccess)  [L223–L234] status=200 [auth=user]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method GetUser [L225]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L232]
        └─ [web] GET /api/fyi/entities/{entityId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetEntity)  [L186–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetEntityQuery -> GetEntityQueryHandler [L191]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntityQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetEntity [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetEntity [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinder)  [L532–L538] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L534]
          └─ query Binder [L534]
            └─ reads_from Binders
        └─ [web] GET /api/reportsettings/status  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.SettingsStatus)  [L72–L85] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L77]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L75]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
        └─ [web] GET /ledger/v1/files/{fileId:Guid}/entities/{entityId:Guid}/datasets  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileEntitiesDatasets)  [L113–L120] status=200
          └─ maps_to DatasetDto [L116]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
            └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
          └─ calls DatasetRepository.ReadQuery [L116]
          └─ query Dataset [L116]
        └─ [web] GET /api/accounting/reports/styles/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Get)  [L41–L53] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ maps_to ReportStyleDto [L44]
            └─ automapper.registration CirrusMappingProfile (ReportStyle->ReportStyleDto) [L580]
          └─ calls ReportStyleRepository.ReadQuery [L44]
          └─ query ReportStyle [L44]
            └─ reads_from ReportStyles
          └─ sends_request GetCustomCssQuery -> GetCustomCssQueryHandler [L49]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetCustomCssQueryHandler.Handle [L30–L86]
              └─ uses_service IStorageService (InstancePerLifetimeScope)
                └─ method GetFileBytes [L53]
                  └─ implementation DataGet.Data.BlobStorage.StorageService.GetFileBytes [L11-L116]
                    └─ uses_service RequestContextProvider
                      └─ method GetContainer [L89]
                        └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
              └─ uses_service IControlledRepository<ReportStyle> (Scoped (inferred))
                └─ method ReadQuery [L43]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportStyleRepository.ReadQuery
              └─ uses_storage IStorageService.GetFileBytes [L53]
        └─ [web] GET /api/matters/bulk  (Workpapers.Next.API.Controllers.Workpapers.MattersController.GetBulk)  [L75–L94] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L90]
          └─ sends_request FindMattersByIdsQuery [L81]
        └─ [web] GET /api/accounting/files/{id}  (Cirrus.Api.Controllers.Accounting.FilesController.Get)  [L90–L94] status=200 [auth=user]
          └─ sends_request GetFileQuery -> GetFileQueryHandler [L93]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.GetFileQueryHandler.Handle [L25–L49]
              └─ maps_to FileDto [L46]
                └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
                └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
              └─ uses_service IControlledRepository<File> (Scoped (inferred))
                └─ method ReadQuery [L44]
                  └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L40]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/users  (Workpapers.Next.API.Controllers.UsersController.GetAllMyFirm)  [L72–L89] status=200
          └─ sends_request GetPagedUsersQuery [L88]
          └─ sends_request GetUsersQuery -> GetUsersQueryHandler [L86]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetUsersQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetUsers [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetUsers [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/accounting-file/{fileId}/legacy-generalledger  (DataGet.Api.Controllers.Connections.AccountingFileController.LegacyGetGeneralLedger)  [L177–L185] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/accounting/reports/output/included-accounts  (Cirrus.Api.Controllers.Accounting.Reports.ReportOutputController.Get)  [L51–L128] status=200 [auth=user]
          └─ maps_to AccountUltraLightDto [L117]
            └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
          └─ calls FileRepository.ReadQuery [L82]
          └─ calls AccountRepository.ReadQuery [L66]
          └─ query File [L82]
            └─ reads_from Files
          └─ query Account [L66]
        └─ [web] GET /api/internal/teams/audit  (Dataverse.Api.Controllers.Internal.Core.TeamsController.GetAll)  [L42–L46] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls TeamRepository.ReadQuery [L45]
          └─ query Team [L45]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L45]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/workpaper-record-template-notes/for-workpaper-record-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.GetNotes)  [L42–L51] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to WorkpaperRecordTemplateNoteDto [L45]
          └─ uses_service UnitOfWork
            └─ method Table [L45]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/ledger/journals/for-dataset/{datasetId:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetForDataset)  [L80–L103] status=200 [auth=user]
          └─ maps_to JournalLightDto [L92]
          └─ calls JournalRepository.ReadQuery [L90]
          └─ query Journal [L90]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L86]
        └─ [web] GET /api/starterPacks/  (Workpapers.Next.API.Controllers.StarterPackController.GetStarterPacks)  [L31–L38] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to StarterPackDto [L34]
          └─ uses_service UnitOfWork
            └─ method Table [L34]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Get)  [L53–L67] status=200 [auth=user]
          └─ maps_to ReportPageTypeDto (var dto) [L64]
          └─ calls ReportContentRepository.LoadReadProperties [L63]
          └─ calls ReportPageTypeRepository.ReadQuery [L56]
          └─ query ReportPageType [L56]
            └─ reads_from ReportPageTypes
          └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
            └─ method LoadReadProperties [L63]
              └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties (see previous expansion)
          └─ sends_request PrepareImagesContentCommand [L65]
        └─ [web] GET /api/ui/imanage/folders  (Dataverse.Api.Controllers.UI.IManageController.GetFolders)  [L252–L268] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L256]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L256]
          └─ query IntegrationSettings [L256]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetFoldersAsync [L262]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetFoldersAsync [L19-L225]
        └─ [web] GET /api/internal/users/intercom-profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.IntercomProfile)  [L111–L139] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service IntercomService
            └─ method GetContactByExternalId [L119]
              └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
                └─ uses_client IntercomClient [L31]
                └─ uses_service IntercomClient
                  └─ method GetContacts [L31]
                    └─ implementation Dataverse.Services.ModuleIntegrations.Clients.IntercomClient.GetContacts [L25-L174]
          └─ uses_service UserService
            └─ method GetUserAsync [L114]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
        └─ [web] GET /api/dataverse/financial-data  (Workpapers.Next.API.Controllers.DataverseController.GetFinancialDataSummary)  [L432–L440] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ sends_request GetBinderFinancialDataQuery -> GetBinderFinancialDataQueryHandler [L437]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderFinancialDataQueryHandler.Handle [L29–L95]
              └─ uses_service UnitOfWork
                └─ method Table [L44]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/fyi/test-connection  (Dataverse.Api.Controllers.UI.FyiController.TestConnection)  [L205–L212] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method TestConnection [L209]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.TestConnection [L19-L291]
        └─ [web] GET /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Get)  [L40–L48] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaxAgentDto [L43]
            └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
            └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
          └─ calls TaxAgentRepository.ReadQuery [L43]
          └─ query TaxAgent [L43]
            └─ reads_from TaxAgents
        └─ [web] GET /api/workpapers/byuser  (Workpapers.Next.API.Controllers.WorkpapersController.GetForUser)  [L51–L55] status=200
        └─ [web] GET /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Search)  [L48–L64] status=200 [auth=user]
          └─ sends_request FindEntitiesQuery -> FindEntitiesQueryHandler [L63]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindEntitiesQueryHandler.Handle [L70–L200]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L129]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L91]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
                └─ method ReadQuery [L89]
                  └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
        └─ [web] GET /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Search)  [L55–L81] status=200 [auth=user]
          └─ sends_request FindFilesQuery -> FindFilesQueryHandler [L69]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindFilesQueryHandler.Handle [L58–L195]
              └─ maps_to UserLightDto [L181]
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L172]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IControlledRepository<User> (Scoped (inferred))
                └─ method ReadQuery [L125]
                  └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
              └─ uses_service IFirmSettingsService (AddScoped)
                └─ method GetCurrentSettings [L122]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service IControlledRepository<File> (Scoped (inferred))
                └─ method ReadQuery [L91]
                  └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L87]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/accounting/reports/page-layouts  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.ConditionalAdminAuth)  [L114–L121] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IAuthorizationService
            └─ method AuthorizeAsync [L116]
              └─ ... (no implementation details available)
        └─ [web] GET /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.GetAll)  [L58–L73] status=200 [auth=user]
          └─ maps_to OfficeLightDto [L68]
            └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
          └─ calls OfficeRepository.ReadQuery [L68]
          └─ query Office [L68]
            └─ reads_from Offices
          └─ uses_service IFirmSettingsService (AddScoped)
            └─ method GetCurrentSettings [L66]
              └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings (see previous expansion)
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method IsInRole [L61]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole [L20-L295]
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
                    └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
                └─ uses_service Guid?
                  └─ method GetUserId [L64]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/super-secure-beneficial-owner/{superSecureId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscSuperSecureBeneficialOwner)  [L354–L362] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscSuperSecureBeneficialOwnerQuery -> GetCompanyPscSuperSecureBeneficialOwnerQueryHandler [L359]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscSuperSecureBeneficialOwnerQueryHandler.Handle [L19–L29]
        └─ [web] GET /api/ui/audit-histories/search  (Dataverse.Api.Controllers.UI.AuditHistories.AuditHistoriesController.Search)  [L26–L55] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindAuditHistoriesQuery -> FindAuditHistoriesQueryHandler [L40]
            └─ handled_by Dataverse.ApplicationService.Queries.AuditHistories.FindAuditHistoriesQueryHandler.Handle [L55–L89]
              └─ calls AuditHistoryRepository.ReadQuery [L70]
        └─ [web] GET /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Get)  [L46–L64] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to PolicyVariantDto (var dto) [L61]
          └─ calls ReportContentRepository.LoadReadProperties [L58]
          └─ calls PolicyVariantRepository.ReadQuery [L49]
          └─ query PolicyVariant [L49]
            └─ reads_from PolicyVariants
          └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
            └─ method LoadReadProperties [L58]
              └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties (see previous expansion)
          └─ sends_request PrepareImagesContentCommand [L62]
        └─ [web] GET /workflow/v1/job-types/  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.MinimalQuery)  [L69–L76] status=200
          └─ calls JobTypeRepository.ReadQuery [L73]
          └─ query JobType [L73]
            └─ reads_from JobTypes
        └─ [web] GET /api/imanage/access-info  (DataGet.Api.Controllers.Integrations.IManageController.GetAccessInfo)  [L115–L136] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
            └─ method GetDisplayIntegrationConfigInfo [L123]
              └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetDisplayIntegrationConfigInfo [L8-L37]
                └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
                  └─ method GetApiIntegrationConfig [L19]
                    └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.GetApiIntegrationConfig (see previous expansion)
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/linked-records  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetLinkedRecords)  [L126–L138] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to LinkedWorkpaperRecordDto [L131]
            └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->LinkedWorkpaperRecordDto) [L559]
          └─ calls WorkpaperRecordRepository.ReadQuery [L131]
          └─ query WorkpaperRecord [L131]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L129]
        └─ [web] GET /api/connections/reportance/ledger/{datasetId}  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetLedger)  [L76–L82] status=200
        └─ [web] GET /ledger/v1/files/  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFilesAsync)  [L48–L59] status=200
          └─ calls FileRepository.ReadQuery [L53]
          └─ query File [L53]
            └─ reads_from Files
        └─ [web] GET /api/accounting/reports/pageTypes/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetUiPackage)  [L123–L134] status=200 [auth=user]
          └─ maps_to FooterTemplateLightDto [L129]
            └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateLightDto) [L628]
          └─ maps_to HeaderTemplateLightDto [L126]
            └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
          └─ calls FooterTemplateRepository.ReadQuery [L129]
          └─ calls HeaderTemplateRepository.ReadQuery [L126]
          └─ query FooterTemplate [L129]
            └─ reads_from ReportFooterTemplates
          └─ query HeaderTemplate [L126]
            └─ reads_from ReportHeaderTemplates
        └─ [web] GET /api/ui/workflow/task-template-groups  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetAll)  [L34–L43] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaskTemplateGroupDto [L37]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
            └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
          └─ calls TaskTemplateGroupRepository.ReadQuery [L37]
          └─ query TaskTemplateGroup [L37]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stages  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStages)  [L60–L68] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetStageForAutomationRunWithDataQuery [L67]
          └─ sends_request GetStageForAutomationRunQuery -> GetStagesForAutomationRunQueryHandler [L66]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStagesForAutomationRunQueryHandler.Handle [L49–L138]
              └─ maps_to AutomationRunStageDto [L70]
                └─ converts_to AutomationRunStageModel [L961]
              └─ maps_to AutomationRunDocumentDto [L99]
                └─ converts_to AutomationRunDocumentModel [L968]
              └─ maps_to AutomationRunDatumDto [L95]
                └─ converts_to AutomationRunDatumModel [L965]
              └─ maps_to AutomationRunRecordDto [L91]
                └─ converts_to AutomationRunRecordModel [L964]
              └─ maps_to AutomationRunJournalDto [L87]
                └─ converts_to AutomationRunJournalModel [L963]
              └─ maps_to AutomationRunAccountDto [L83]
                └─ converts_to AutomationRunAccountModel [L962]
              └─ uses_service IControlledRepository<AutomationRun> (Scoped (inferred))
                └─ method ReadQuery [L122]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.AutomationRunRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L63]
        └─ [web] GET /api/account-types/search  (Workpapers.Next.API.Controllers.AccountTypesController.Search)  [L59–L65] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request FindAccountTypesQuery -> FindAccountTypesQueryHandler [L64]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.FindAccountTypesQueryHandler.Handle [L41–L80]
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
              └─ uses_service IControlledRepository<AccountType> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountTypeRepository.ReadQuery
          └─ sends_request FindAccountTypesLightQuery -> FindAccountTypesQueryHandler [L63]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindAccountTypesQueryHandler.Handle [L50–L82]
              └─ calls AccountTypeRepository.ReadQuery [L78]
        └─ [web] GET /api/internal/offices/audit  (Dataverse.Api.Controllers.Internal.Core.OfficesController.GetAll)  [L41–L45] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls OfficeRepository.ReadQuery [L44]
          └─ query Office [L44]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L44]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /workflow/v1/task-template-groups/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.MinimalQuery)  [L66–L73] status=200
          └─ calls TaskTemplateGroupRepository.ReadQuery [L70]
          └─ query TaskTemplateGroup [L70]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/ui/documents/documents/associations  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentAssociations)  [L149–L167] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDocumentAssociations -> GetDocumentAssociationsHandler [L163]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentAssociationsHandler.Handle [L39–L148]
              └─ maps_to DeliverableUltraLightDto [L98]
                └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableUltraLightDto) [L355]
              └─ maps_to JobUltraLightDto [L86]
                └─ automapper.registration DataverseMappingProfile (Job->JobUltraLightDto) [L306]
              └─ uses_client WorkpapersClient [L119]
              └─ uses_service WorkpapersClient
                └─ method Get [L119]
                  └─ ... (no implementation details available)
              └─ uses_service UserService
                └─ method GetUserId [L117]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L114]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_service IControlledRepository<Deliverable> (Scoped (inferred))
                └─ method ReadQuery [L98]
                  └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery
              └─ uses_service IControlledRepository<Job> (Scoped (inferred))
                └─ method ReadQuery [L86]
                  └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L74]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L159]
        └─ [web] GET /api/companies-house/search/alphabetical-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompaniesAlphabetically)  [L70–L79] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchCompaniesAlphabeticallyQuery -> SearchCompaniesAlphabeticallyQueryHandler [L76]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesAlphabeticallyQueryHandler.Handle [L23–L34]
        └─ [web] GET /api/internal/firm-settings/  (Dataverse.Api.Controllers.Internal.Firm.FirmSettingsController.Get)  [L38–L60] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to IntegrationSettingsDto [L55]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ maps_to FirmSettingsDto (var settings) [L53]
          └─ maps_to FirmSettingsDto [L48]
            └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
          └─ maps_to FirmSettingsDto (var defaultSettings) [L44]
          └─ calls IntegrationSettingsRepository.ReadQuery [L55]
          └─ calls FirmSettingsRepository.ReadQuery [L48]
          └─ query IntegrationSettings [L55]
            └─ reads_from IntegrationSettingss
          └─ query FirmSettings [L48]
            └─ reads_from FirmSettingss
          └─ uses_service TenantService
            └─ method GetCurrentTenant [L42]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
        └─ [web] GET /api/workpaper-records/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordsForWorksheet)  [L153–L165] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorkpaperRecordLightDto [L159]
            └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordLightDto) [L515]
          └─ calls WorkpaperRecordRepository.ReadQuery [L159]
          └─ query WorkpaperRecord [L159]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L157]
        └─ [web] GET /api/fyi/cabinets/{cabinetId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinet)  [L56–L65] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCabinetQuery -> GetCabinetQueryHandler [L61]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetCabinet [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCabinet [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/ato/gov-link/client-account-transactions  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshClientAccountTransactions)  [L136–L147] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetClientAccountTransactionsQuery -> GetClientAccountTransactionsQueryHandler [L143]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetClientAccountTransactionsQueryHandler.Handle [L36–L77]
              └─ uses_client DataGetClient [L64]
                └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetClientAccountDetailsAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/client-accounts/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetail)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetClientAccountDetailsAsync [L64]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetClientAccountDetailsAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L55]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/worksheets  (Workpapers.Next.API.Controllers.WorksheetsController.GetForProduct)  [L61–L69] status=200
          └─ uses_service UserService
            └─ method GetFirmId [L64]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query -> AllWorkpaperRecordTemplatesForProductQueryHandler [L66]
            └─ handled_by Workpapers.Next.Data.Queries.Templates.AllWorkpaperRecordTemplatesForProductQueryHandler.Handle [L18–L141]
              └─ maps_to WorkpaperRecordTemplateV2Dto [L88]
                └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
                └─ converts_to LegacyDocumentDto [L343]
              └─ maps_to SectionWithWorkpaperRecordTemplateIdsDto [L54]
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L69]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<ExcludedWorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L65]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedWorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<Product> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Workpapers.Next.Data.Repository.Licensing.ProductRepository.ReadQuery
          └─ sends_request GetProductQuery -> GetProductQueryHandler [L64]
        └─ [web] GET /api/clients/and-entities  (Cirrus.Api.Controllers.Firm.ClientsController.SearchClientAndEntity)  [L84–L88] status=200 [auth=user]
          └─ sends_request FindClientsAndEntitiesQuery -> FindClientsAndEntitiesQueryHandler [L87]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsAndEntitiesQueryHandler.Handle [L55–L138]
              └─ maps_to ClientEntitySearchDto [L122]
                └─ automapper.registration CirrusMappingProfile (Client->ClientEntitySearchDto) [L148]
              └─ uses_service IControlledRepository<Client> (Scoped (inferred))
                └─ method ReadQuery [L122]
                  └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
              └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
                └─ method ReadQuery [L108]
                  └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUser [L78]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L75]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings (see previous expansion)
        └─ [web] GET /api/binder-index/account-balance-info  (Workpapers.Next.API.Controllers.Workpapers.BinderIndexController.GetIndexAccountBalanceInfo)  [L23–L27] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetIndexAccountBalanceInfoQuery -> GetIndexAccountBalanceInfoQueryHandler [L26]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetIndexAccountBalanceInfoQueryHandler.Handle [L36–L358]
              └─ calls SourceRepository.ReadQuery [L129]
              └─ calls SourceAccountRepository.ReadQuery [L74]
              └─ maps_to AccountBalanceInfoDto [L65]
                └─ automapper.registration WorkpapersMappingProfile (Binder->AccountBalanceInfoDto) [L457]
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L65]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L63]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/worksheets/UserWorksheets  (Workpapers.Next.API.Controllers.LegacyController.GetForProduct)  [L50–L64] status=200
          └─ maps_to LegacyDocumentDto [L61]
          └─ uses_service UserService
            └─ method GetFirmId [L53]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query -> AllWorkpaperRecordTemplatesForProductQueryHandler [L58]
          └─ sends_request GetProductQuery -> GetProductQueryHandler [L53]
        └─ [web] GET /documents/v1/documents/detailed  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.FullQuery)  [L104–L110] status=200
          └─ maps_to DocumentDto [L107]
            └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
          └─ calls DocumentRepository.ReadQuery [L107]
          └─ query Document [L107]
            └─ reads_from Documents
        └─ [web] GET /api/connection/{apiType}  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnection)  [L40–L44] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetConnectionQuery -> GetConnectionQueryHandler [L43]
            └─ handled_by DataGet.Connections.App.Queries.GetConnectionQueryHandler.Handle [L20–L48]
              └─ calls ConnectionRepository.ReadQuery [L40]
              └─ maps_to ConnectionDto [L40]
              └─ uses_service RequestContextProvider
                └─ method CurrentContext [L38]
                  └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.CurrentContext
        └─ [web] GET /api/ui/templates/{id}/download  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.DownloadTemplate)  [L66–L74] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ITemplateService (AddSingleton)
            └─ method GetTemplateDownloadUrl [L71]
              └─ implementation Dataverse.Templates.TemplateService.GetTemplateDownloadUrl [L17-L359]
                └─ uses_service TemplateConfiguration
                  └─ method GetSiteDriveId [L333]
                    └─ ... (no implementation details available)
                └─ uses_service SharePointTemplateManager
                  └─ method GetDownloadUrl [L82]
                    └─ ... (no implementation details available)
                └─ uses_cache IDistributedCache.SetRecordAsync [write] [L47]
                └─ uses_cache IDistributedCache.GetRecordAsync [read] [L41]
        └─ [web] GET /core/v1/offices/  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.MinimalQuery)  [L69–L74] status=200
          └─ calls OfficeRepository.ReadQuery [L72]
          └─ query Office [L72]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L72]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/account-mapping/external-reporting-systems  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystems)  [L45–L49] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetExternalReportingSystemsQuery -> GetExternalReportingSystemsQueryHandler [L48]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemsQueryHandler.Handle [L29–L56]
              └─ maps_to ExternalReportingSystemLightDto [L52]
              └─ uses_service UserService
                └─ method IsMaster [L46]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster (see previous expansion)
              └─ uses_service IControlledRepository<ExternalReportingSystem> (Scoped (inferred))
                └─ method ReadQuery [L44]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemRepository.ReadQuery
        └─ [web] GET /api/health  (DataGet.Api.Controllers.HealthController.Alive)  [L10–L14] status=200 [AllowAnonymous]
        └─ [web] GET /workflow/v1/job-statuses/  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.MinimalQuery)  [L69–L76] status=200
          └─ calls JobStatusRepository.ReadQuery [L73]
          └─ query JobStatus [L73]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /core/v1/clients/entities  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityMinimalQuery)  [L91–L99] status=200
          └─ uses_client ClientRepository [L94]
        └─ [web] GET /api/accounting/assets/depreciation-years/  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.GetAll)  [L54–L66] status=200 [auth=user]
          └─ maps_to DepreciationYearDto [L61]
            └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
          └─ calls DepreciationYearRepository.ReadQuery [L61]
          └─ query DepreciationYear [L61]
            └─ reads_from DepreciationYears
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L60]
        └─ [web] GET /api/companies-house/search/officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchOfficers)  [L50–L58] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchOfficersQuery -> SearchOfficersQueryHandler [L55]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchOfficersQueryHandler.Handle [L21–L32]
        └─ [web] GET /api/companies-house/company/{companyNumber}/exemptions  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyExemptions)  [L213–L221] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyExemptionsQuery -> GetCompanyExemptionsQueryHandler [L218]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyExemptionsQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetHyperlinks)  [L285–L305] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to HyperlinkDto [L296]
          └─ calls WorkpaperRecordRepository.ReadQuery [L296]
          └─ calls WorksheetRepository.ReadQuery [L290]
          └─ query WorkpaperRecord [L296]
            └─ reads_from WorkpaperRecords
          └─ query Worksheet [L290]
            └─ reads_from Worksheets
          └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L288]
        └─ [web] GET /api/ui/workflow/job-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetById)  [L66–L74] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobTypeDto [L69]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
            └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
          └─ calls JobTypeRepository.ReadQuery [L69]
          └─ query JobType [L69]
            └─ reads_from JobTypes
        └─ [web] GET /api/ui/sync-configuration/{id:guid}/tenants  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetTenants)  [L197–L201] status=200 [auth=Authentication.AdminPolicy]
        └─ [web] GET /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.GetAll)  [L46–L55] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobStatusLightDto [L49]
            └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusLightDto) [L316]
          └─ calls JobStatusRepository.ReadQuery [L49]
          └─ query JobStatus [L49]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/claims/isTrial/{tenantId}/product/{productType}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetIsTrialClaim)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ calls TenantRepository.ReadTable [L118]
          └─ query Tenant [L118]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L118]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/one-platform/clients/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetClientId)  [L30–L39] status=200 [auth=Authentication.UserPolicy]
          └─ calls ClientRepository.ReadQuery [L33]
          └─ query Client [L33]
        └─ [web] GET /api/connection/{apiType}/{email}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnectionWithEmail)  [L121–L130] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request TestConnectionCommand -> TestConnectionCommandHandler [L125]
            └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L47]
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                └─ method GetExternalApiFromApiType [L38]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForDataset)  [L63–L84] status=200 [auth=user]
          └─ calls DatasetRepository.ReadQuery [L71]
          └─ query Dataset [L71]
          └─ sends_request GetSourceAccountsWithCachedQuery -> GetSourceAccountsWithCachedQueryHandler [L80]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
              └─ maps_to SourceAccountWithCachedDto [L53]
                └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
              └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L68]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L53]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L70]
        └─ [web] GET /api/ui/sync-configuration/{id:guid}/test-connection  (Dataverse.Api.Controllers.UI.SyncConfigurationController.TestConnection)  [L203–L207] status=200 [auth=Authentication.AdminPolicy]
        └─ [web] GET /api/accounting/assets/asset-groups/  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.GetAll)  [L53–L62] status=200 [auth=user]
          └─ maps_to AssetGroupDto [L57]
            └─ automapper.registration CirrusMappingProfile (AssetGroup->AssetGroupDto) [L874]
          └─ calls AssetGroupRepository.ReadQuery [L57]
          └─ query AssetGroup [L57]
            └─ reads_from AssetGroups
        └─ [web] GET /api/ui/fyi/cabinets  (Dataverse.Api.Controllers.UI.FyiController.GetCabinets)  [L47–L53] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetCabinetsAsync [L50]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCabinetsAsync [L19-L291]
        └─ [web] GET /api/fyi/entities  (DataGet.Api.Controllers.Integrations.FyiController.GetEntities)  [L175–L184] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetEntitiesQuery -> GetEntitiesQueryHandler [L180]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntitiesQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetEntities [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetEntities [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /webhooks/v1/webhooks/  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.FullQuery)  [L85–L92] status=200 [auth=Authentication.CoreRead]
          └─ calls WebhookRepository.ReadQuery [L90]
          └─ query Webhook [L90]
            └─ reads_from Webhooks
        └─ [web] GET /api/workpaperitems/byworkbook/{workbookId}/{worksheetId}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorksheet)  [L139–L149] status=200
          └─ maps_to WorkpaperItemDto [L142]
          └─ uses_service UnitOfWork
            └─ method Table [L142]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/workflow/tasks  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetTaskManager)  [L205–L218] status=200 [auth=Authentication.UserPolicy]
          └─ calls TaskRepository.WriteQuery [L207]
          └─ uses_service TaskRepository
            └─ method WriteQuery [L207]
              └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.WriteQuery [L8-L40]
          └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L215]
        └─ [web] GET /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Get)  [L99–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ maps_to UserDto [L102]
            └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
          └─ calls UserRepository.ReadQuery [L102]
          └─ query User [L102]
        └─ [web] GET /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetAll)  [L33–L43] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to KanbanColumnDto [L36]
            └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
            └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
          └─ calls KanbanColumnRepository.ReadQuery [L36]
          └─ query KanbanColumn [L36]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/proposed-items/record-templates  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetProposedRecordTemplates)  [L49–L77] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ProposedRecordTemplateDto [L54]
            └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedRecordTemplateDto) [L985]
          └─ calls ProposedItemRepository.ReadQuery [L54]
          └─ query ProposedItem [L54]
            └─ reads_from ProposedItems
          └─ sends_request FilterValidRecordTemplatesForBinderQuery -> GetValidRecordTemplatesForBinderQueryHandler [L69]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetValidRecordTemplatesForBinderQueryHandler.Handle [L34–L103]
              └─ uses_service UnitOfWork
                └─ method Table [L75]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service UserService
                └─ method GetFirmId [L71]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L65]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L59]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L52]
        └─ [web] GET /api/accounting/assets/reports/full-summary  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetFullSummaryReport)  [L52–L64] status=200 [auth=user]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L55]
        └─ [web] GET /api/internal/users/audit  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAudit)  [L77–L81] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls UserRepository.ReadQuery [L80]
          └─ query User [L80]
          └─ uses_service UserRepository
            └─ method ReadQuery [L80]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/super/workpapers  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetRegionCodeAsync)  [L145–L148] status=200 [auth=Authentication.MasterPolicy]
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L147]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /api/ui/fyi/access-info  (Dataverse.Api.Controllers.UI.FyiController.GetAccessInfo)  [L182–L189] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetAccessInfo [L186]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetAccessInfo [L19-L291]
        └─ [web] GET /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Get)  [L77–L83] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to OfficeDto [L80]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
            └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
          └─ calls OfficeRepository.ReadQuery [L80]
          └─ query Office [L80]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L80]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.GetAll)  [L41–L50] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to BinderAutomationRuleDto [L44]
            └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
          └─ calls BinderAutomationRuleRepository.ReadQuery [L44]
          └─ query BinderAutomationRule [L44]
            └─ reads_from BinderAutomationRules
        └─ [web] GET /api/binder-document-defaults/for-binder/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.GetByBinderType)  [L44–L48] status=200
          └─ sends_request GetBinderTemplateDocumentDefaultsQuery [L47]
        └─ [web] GET /api/internal/connections/{apiType}/tenants  (DataGet.Api.Controllers.Internal.InternalController.GetConnectedTenantIds)  [L21–L28] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /audit  (Dataverse.Api.Controllers.External.ContactsController.GetAllAudits)  [L40–L45] status=200
          └─ calls ContactRepository.ReadQuery [L44]
          └─ query Contact [L44]
            └─ reads_from DVS_Clients
        └─ [web] GET /workflow/v1/job-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditsQuery)  [L102–L108] status=200
          └─ maps_to EntityAuditDto [L105]
          └─ calls JobTypeRepository.ReadQuery [L105]
          └─ query JobType [L105]
            └─ reads_from JobTypes
        └─ [web] GET /api/ui/imanage  (Dataverse.Api.Controllers.UI.IManageController.GetDownloadUrl)  [L305–L312] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service StorageService
            └─ method CopyFileFromUri [L309]
              └─ implementation Dataverse.Services.Features.Storage.StorageService.CopyFileFromUri [L18-L331]
                └─ uses_service TenantService
                  └─ method GetPrivateContainer [L305]
                    └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetPrivateContainer [L6-L27]
                      └─ uses_service TenantIdentificationService
                        └─ method GetCurrentTenant [L20]
                          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
                └─ uses_service StorageSettingsConfig
                  └─ method GetBlobServiceClient [L31]
                    └─ implementation StorageSettingsConfig.GetBlobServiceClient
                └─ uses_storage StorageSettingsConfig.GetBlobServiceClient [L31]
          └─ uses_storage StorageService.CopyFileFromUri [L309]
        └─ [web] GET /api/template-standard-accounts/for-set/{setId:guid}  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccountsForSet)  [L60–L70] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to TemplateStandardAccountDto [L64]
            └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccount->TemplateStandardAccountDto) [L674]
          └─ calls TemplateStandardAccountRepository.ReadQuery [L64]
          └─ query TemplateStandardAccount [L64]
            └─ reads_from TemplateStandardAccounts
        └─ [web] GET /api/users/intercom-profile  (Workpapers.Next.API.Controllers.UsersController.GetIntercomProfile)  [L149–L155] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetIntercomProfileQuery -> GetIntercomProfileQueryHandler [L153]
            └─ handled_by Cirrus.Central.Dataverse.Queries.GetIntercomProfileQueryHandler.Handle [L24–L61]
              └─ uses_service DataverseService
                └─ method Get [L52]
                  └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L42]
                  └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L39]
                  └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant (see previous expansion)
          └─ returns IntercomProfileDto [L153]
        └─ [web] GET /api/companies-house-gateway/payment-periods  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetPaymentPeriodsAsync)  [L48–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetPaymentPeriodsQuery -> GetPaymentPeriodsQueryHandler [L51]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetPaymentPeriodsQueryHandler.Handle [L33–L61]
              └─ maps_to PaymentPeriodsResponseDto [L59]
              └─ uses_client CompaniesHouseInputGatewayClient [L55]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L55]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L54]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/ledger/cashflow/categories/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.GetCategories)  [L57–L84] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowCategoryLightDto [L79]
            └─ automapper.registration CirrusMappingProfile (CashflowCategory->CashflowCategoryLightDto) [L466]
          └─ calls CashflowCategoryRepository.ReadQuery [L79]
          └─ query CashflowCategory [L79]
            └─ reads_from CashflowCategories
          └─ uses_service IReadRepository (InstancePerLifetimeScope)
            └─ method Table [L70]
              └─ ... (no implementation details available)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L66]
        └─ [web] GET /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetById)  [L54–L62] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DeliverableTypeLightDto [L57]
            └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L359]
          └─ calls DeliverableTypeRepository.ReadQuery [L57]
          └─ query DeliverableType [L57]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/accounting/reports/generate-content/insert  (Cirrus.Api.Controllers.Accounting.Reports.GenerateReportContentController.GenerateInsert)  [L23–L33] status=200 [auth=user]
        └─ [web] GET /api/sections/byproductnumber/{id:int}  (Workpapers.Next.API.Controllers.SectionsController.GetByProductId)  [L65–L81] status=200
          └─ maps_to SectionDto [L68]
          └─ uses_service UnitOfWork
            └─ method Table [L68]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/connections/class/funds/{fundCode}/trial-balance/{date:datetime}  (Workpapers.Next.API.Controllers.Connections.ClassController.GetTrialBalance)  [L44–L51] status=200
        └─ [web] GET /core/v1/clients/audits  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetAuditsQuery)  [L242–L248] status=200
          └─ maps_to EntityAuditDto [L245]
          └─ uses_client ClientRepository [L245]
        └─ [web] GET /api/ui/documents/documents/{documentId}/check-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckDocumentExternalVersion)  [L460–L465] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request CheckNewVersionFromExternalSourceCommand -> CheckNewVersionFromExternalSourceCommandHandler [L463]
            └─ handled_by Dataverse.Connections.DataGet.Commands.CheckNewVersionFromExternalSourceCommandHandler.Handle [L36–L161]
              └─ maps_to IntegrationSettingsDto [L118]
                └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
              └─ uses_service DataGetImanageService
                └─ method GetDocumentVersion [L125]
                  └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentVersion [L19-L225]
              └─ uses_service IControlledRepository<IntegrationSettings> (Scoped (inferred))
                └─ method ReadQuery [L118]
                  └─ implementation Dataverse.Data.Repository.Firm.IntegrationSettingsRepository.ReadQuery
              └─ uses_service DataGetFyiService
                └─ method GetExternalDocumentAsync [L104]
                  └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetExternalDocumentAsync [L19-L291]
              └─ uses_service IControlledRepository<ExternalEntityVersion> (Scoped (inferred))
                └─ method ReadQuery [L73]
                  └─ implementation Dataverse.Data.Repository.Documents.ExternalEntityVersionRepository.ReadQuery
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L90]
              └─ uses_cache IDistributedCache.TryGetRecordAsync [read] [L68]
        └─ [web] GET /workflow/v1/deliverables/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.MinimalQuery)  [L69–L77] status=200
          └─ calls DeliverableRepository.ReadQuery [L73]
          └─ query Deliverable [L73]
            └─ reads_from Deliverables
        └─ [web] GET /api/internal/accounting/files/exist-for-client/{dataverseClientId:guid}  (Cirrus.Api.Controllers.Internal.FilesController.CheckForClientFiles)  [L38–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls FileRepository.ReadQuery [L41]
          └─ query File [L41]
            └─ reads_from Files
        └─ [web] GET /api/worksheets/download  (Workpapers.Next.API.Controllers.WorksheetsController.Download)  [L83–L99] status=200
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L95]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UserService
            └─ method GetUser [L86]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L95]
          └─ sends_request GetTemplateForDateTimeQuery -> GetTemplateForDateTimeQueryHandler [L86]
        └─ [web] GET /api/accounting/connections/{type}/authorisation-url  (Cirrus.Api.Controllers.Accounting.ConnectionsController.GetAuthorisationUrl)  [L37–L42] status=200 [auth=user]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetApiMethods [L40]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/dataverse/{entityRoute}/audit  (Cirrus.Api.Controllers.Dataverse.DataverseController.GetData)  [L89–L96] status=200 [auth=Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/accounting/reports/saved-reports/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.GetAll)  [L82–L92] status=200 [auth=user]
          └─ maps_to SavedReportFileDto [L86]
            └─ automapper.registration CirrusMappingProfile (PublishedReportFile->SavedReportFileDto) [L594]
          └─ calls PublishedReportFileRepository.ReadQuery [L86]
          └─ query PublishedReportFile [L86]
            └─ reads_from PublishedReportFiles
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L85]
        └─ [web] GET /workflow/v1/deliverable-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.FullQuery)  [L88–L95] status=200
          └─ calls DeliverableTypeRepository.ReadQuery [L92]
          └─ query DeliverableType [L92]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/tenant/databases/regions  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetRegions)  [L54–L68] status=200 [auth=master]
          └─ calls TenantRepository.ReadTable [L57]
          └─ query Tenant [L57]
            └─ reads_from Tenants
          └─ uses_service UserService
            └─ method GetIdentityId [L61]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId (see previous expansion)
          └─ uses_service TenantRepository
            └─ method ReadTable [L57]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/ui/karbon/{id}/test-and-update-connection-settings  (Dataverse.Api.Controllers.UI.KarbonController.TestAndUpdateConnectionSettings)  [L50–L74] status=200 [auth=Authentication.AdminPolicy]
          └─ calls SyncConfigurationRepository.WriteQuery [L57]
          └─ write SyncConfiguration [L57]
            └─ reads_from SyncConfigurations
          └─ uses_service UserService
            └─ method GetUserAsync [L59]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
          └─ uses_service IDatagetKarbonService (AddTransient)
            └─ method TestConnection [L53]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetKarbonService.TestConnection [L13-L53]
        └─ [web] GET /api/accounting/ledger/accounts/child/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetChildAccount)  [L80–L84] status=200 [auth=user]
          └─ sends_request GetChildAccountQuery [L83]
        └─ [web] GET /api/accounting/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetAccountHierarchy)  [L94–L102] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}/full-record  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualFullRecord)  [L314–L322] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscIndividualFullRecordQuery -> GetCompanyPscIndividualFullRecordQueryHandler [L319]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualFullRecordQueryHandler.Handle [L19–L31]
        └─ [web] GET /core/v1/clients/  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityOrGroupMinimalQuery)  [L58–L65] status=200
          └─ uses_client ClientRepository [L61]
        └─ [web] GET /loaderio-ea179ca138937c6992ea6d4e6e516fe6  (Cirrus.Api.Controllers.LoadTestController.NewLoaderIo)  [L16–L20] status=200 [AllowAnonymous]
        └─ [web] GET /workflow/v1/job-statuses/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditQuery)  [L116–L121] status=200
          └─ maps_to EntityAuditDto [L119]
          └─ calls JobStatusRepository.ReadQuery [L119]
          └─ query JobStatus [L119]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/accounting/sourcedata/sources/{id}/divisions  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetDivisions)  [L70–L74] status=200 [auth=user]
          └─ sends_request GetSourceDivisionsQuery -> GetSourceDivisionsQueryHandler [L73]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Api.GetSourceDivisionsQueryHandler.Handle [L31–L55]
              └─ maps_to SourceLightDto [L48]
                └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L50]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service ApiService (SingleInstance)
                └─ method GetApiMethods [L49]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
              └─ uses_service IControlledRepository<Source> (Scoped (inferred))
                └─ method ReadQuery [L48]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.ReadQuery
        └─ [web] GET /api/one-platform/entities/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetEntityId)  [L41–L51] status=200 [auth=Authentication.UserPolicy]
          └─ calls EntityRepository.ReadQuery [L44]
          └─ query Entity [L44]
        └─ [web] GET /api/binders/{id:Guid}/export-details  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetBinderExportedDetails)  [L462–L473] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ExportedBinderDetail [L468]
            └─ automapper.registration WorkpapersMappingProfile (Binder->ExportedBinderDetail) [L472]
          └─ calls BinderRepository.ReadQuery [L468]
          └─ query Binder [L468]
            └─ reads_from Binders
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L466]
        └─ [web] GET /api/stats/topusers  (Workpapers.Next.API.Controllers.StatsController.TopUsers)  [L215–L226] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TopUsers-type{*}-period{*}) [L218]
          └─ sends_request GetTopUsersQuery -> GetTopUsersQueryHandler [L222]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopUsersQueryHandler.Handle [L32–L91]
              └─ uses_client KeenClient [L64]
              └─ uses_service UnitOfWork
                └─ method Table [L74]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service KeenQueryBuilder
                └─ method Build [L65]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L64]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/styles/word/download/master  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWordController.DownloadMaster)  [L41–L49] status=200 [auth=Authentication.AdminPolicy]
        └─ [web] GET /api/internal/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy  (Cirrus.Api.Controllers.Internal.StandardChartController.GetAccountsHierarchy)  [L46–L71] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to StandardChartDto [L54]
            └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
            └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
          └─ calls StandardChartRepository.ReadQuery [L54]
          └─ query StandardChart [L54]
            └─ reads_from StandardCharts
        └─ [web] GET /api/companies-house/document/{documentId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetDocumentMetadata)  [L398–L406] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentMetadataQuery -> GetDocumentMetadataQueryHandler [L403]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentMetadataQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/ui/messages/  (Dataverse.Api.Controllers.UI.MessagesController.GetMessages)  [L29–L35] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetMessagesQuery -> GetMessagesQueryHandler [L32]
            └─ handled_by Dataverse.ApplicationService.Queries.Messaging.GetMessagesQueryHandler.Handle [L39–L102]
              └─ calls UserRepository.ReadQuery [L81]
              └─ maps_to UserLightDto [L99]
              └─ uses_service IControlledRepository<Message> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Dataverse.Data.Repository.Users.MessageRepository.ReadQuery
        └─ [web] GET /api/ui/users/getAll  (Dataverse.Api.Controllers.UI.Core.UsersController.GetAll)  [L169–L177] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to UserLightDto [L172]
            └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
          └─ calls UserRepository.ReadQuery [L172]
          └─ query User [L172]
          └─ uses_service UserRepository
            └─ method ReadQuery [L172]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/fyi/documents/{documentVersionId:guid}/authorise-upload  (Dataverse.Api.Controllers.UI.FyiController.AuthoriseUpload)  [L114–L120] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method AuthoriseUploadAsync [L117]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.AuthoriseUploadAsync [L19-L291]
        └─ [web] GET /documents/v1/documents/{id:Guid}/download  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.DownloadDocument)  [L70–L76] status=200
          └─ sends_request GetDocumentDownloadLink -> GetDocumentDownloadLinkHandler [L73]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
              └─ maps_to SecureDocumentStoreDto [L65]
              └─ uses_service UserService
                └─ method GetUserId [L73]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<DocumentAuditLog> (Scoped (inferred))
                └─ method Add [L68]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentAuditLogRepository.Add
              └─ uses_service DocumentServiceFactory
                └─ method GetForStore [L65]
                  └─ implementation DocumentServiceFactory.GetForStore
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
        └─ [web] GET /api/imanage  (DataGet.Api.Controllers.Integrations.IManageController.SetConnectionContext)  [L298–L301] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /workflow/v1/job-statuses/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.Get)  [L52–L57] status=200
          └─ maps_to JobStatusDto [L55]
            └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
            └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
          └─ calls JobStatusRepository.ReadQuery [L55]
          └─ query JobStatus [L55]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/connections/xero/debtors  (Workpapers.Next.API.Controllers.Connections.XeroController.GetDebtors)  [L62–L68] status=200
        └─ [web] GET /api/users/profile  (Workpapers.Next.API.Controllers.UsersController.GetMyProfile)  [L131–L147] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to UserProfileDto [L135]
            └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
          └─ calls UserRepository.ReadQuery [L135]
          └─ query User [L135]
          └─ uses_service FirmFeatureFlagService
            └─ method GetAvailableFeaturesForFirm [L144]
              └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L110]
                └─ calls FirmFeatureFlagRepository.ReadQuery [L107]
                └─ uses_service FirmSettingsService
                  └─ method IsFirmPartOfControlledBeta [L94]
                    └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.IsFirmPartOfControlledBeta (see previous expansion)
                └─ uses_service FeatureFlagService
                  └─ method CanIUseFeature [L80]
                    └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FeatureFlagService.CanIUseFeature (see previous expansion)
                └─ uses_service UserService
                  └─ method CanIUseFeature [L61]
                    └─ implementation Workpapers.Next.ApplicationService.Services.UserService.CanIUseFeature (see previous expansion)
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettings [L137]
              └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L135]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UserRepository
            └─ method ReadQuery [L135]
              └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery (see previous expansion)
          └─ sends_request GetDataverseProfileQuery -> GetDataverseProfileQueryHandler [L139]
            └─ handled_by Cirrus.Central.Dataverse.Queries.GetDataverseProfileQueryHandler.Handle [L25–L66]
              └─ uses_service FirmSettingsService
                └─ method StoreSettings [L57]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.StoreSettings [L15-L112]
                    └─ uses_service IRequestProcessor (InstancePerDependency)
                      └─ method GetCurrentSettings [L45]
                        └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings (see previous expansion)
                    └─ uses_service TenantService
                      └─ method GetCurrentSettings [L34]
                        └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings (see previous expansion)
              └─ uses_service DataverseService
                └─ method Get [L56]
                  └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L46]
                  └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L43]
                  └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant (see previous expansion)
          └─ returns DataverseProfileDto [L139]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/super-secure/{superSecureId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscSuperSecurePerson)  [L364–L372] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscSuperSecurePersonQuery -> GetCompanyPscSuperSecurePersonQueryHandler [L369]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscSuperSecurePersonQueryHandler.Handle [L19–L29]
        └─ [web] GET /api/central/firms/{dataverseId}  (Cirrus.Api.Controllers.Central.FirmController.Get)  [L63–L72] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to FirmDto [L66]
            └─ converts_to FirmWithStatsDto [L162]
          └─ calls CentralRepository.ReadTable [L66]
          └─ uses_service CentralRepository
            └─ method ReadTable [L66]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.GetCategory)  [L46–L55] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowCategoryDto [L54]
          └─ calls CashflowCategoryRepository.ReadQuery [L49]
          └─ query CashflowCategory [L49]
            └─ reads_from CashflowCategories
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L52]
        └─ [web] GET /api/internal/storage/documents/{documentId:guid}/download-url  (Dataverse.Api.Controllers.Internal.StorageController.DownloadColdDocument)  [L88–L94] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetDocumentColdDownloadLink -> GetDocumentColdDownloadLinkHandler [L91]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentColdDownloadLinkHandler.Handle [L28–L74]
              └─ uses_service DocumentServiceFactory
                └─ method GetDefaultColdStorageDocumentWithService [L58]
                  └─ implementation DocumentServiceFactory.GetDefaultColdStorageDocumentWithService (see previous expansion)
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method ReadQuery [L46]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.ReadQuery
        └─ [web] GET /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobTypeDto [L45]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
            └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
          └─ calls JobTypeRepository.ReadQuery [L45]
          └─ query JobType [L45]
            └─ reads_from JobTypes
        └─ [web] GET /api/master-accounts/search  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Search)  [L76–L80] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ sends_request FindMasterAccountsQuery -> FindMasterAccountsQueryHandler [L79]
            └─ handled_by Cirrus.ApplicationService.MasterAccounting.Queries.FindMasterAccountsQueryHandler.Handle [L42–L100]
              └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
                └─ method ReadQuery [L63]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
              └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
                └─ method ReadQuery [L58]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.ReadQuery
        └─ [web] GET /api/gov-link/individual-income-tax-returns/summary  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportSummary)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Get)  [L51–L56] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to PolicyLayoutDto [L54]
            └─ automapper.registration CirrusMappingProfile (PolicyLayout->PolicyLayoutDto) [L783]
          └─ calls PolicyLayoutRepository.ReadQuery [L54]
          └─ query PolicyLayout [L54]
            └─ reads_from PolicyLayouts
        └─ [web] GET /api/accounting/matching-rule-sets/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.GetAll)  [L43–L52] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to MatchingRuleSetDto [L46]
            └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
          └─ calls MatchingRuleSetRepository.ReadQuery [L46]
          └─ query MatchingRuleSet [L46]
            └─ reads_from MatchingRuleSets
        └─ [web] GET /api/proposed-items/automation-bots/for-account  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetAllProposedAutomatonBotsForSourceAccount)  [L98–L115] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ProposedAutomationRunDto [L105]
            └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedAutomationRunDto) [L974]
          └─ calls ProposedItemRepository.ReadQuery [L105]
          └─ calls BinderRepository.ReadQuery [L102]
          └─ query ProposedItem [L105]
            └─ reads_from ProposedItems
          └─ query Binder [L102]
            └─ reads_from Binders
        └─ [web] GET /api/matters  (Workpapers.Next.API.Controllers.Workpapers.MattersController.GetParams)  [L232–L240] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service UserService
            └─ method GetUserId [L239]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
            └─ method ReadQuery [L235]
              └─ ... (no implementation details available)
        └─ [web] GET /api/internal/documents/{id:Guid}  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetDocumentById)  [L296–L300] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetDocumentByIdQuery -> GetDocumentByIdQueryHandler [L299]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentByIdQueryHandler.Handle [L25–L51]
              └─ maps_to DocumentLightDto [L45]
                └─ automapper.registration DataverseMappingProfile (Document->DocumentLightDto) [L401]
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L45]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L43]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/dataverse/subscription-info  (Workpapers.Next.API.Controllers.DataverseController.GetSubscriptionCreateInfo)  [L187–L192] status=200 [auth=AuthorizationPolicies.M2M]
          └─ sends_request GetSubscriptionInfoQuery -> GetSubscriptionInfoQueryHandler [L191]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Dataverse.GetSubscriptionInfoQueryHandler.Handle [L33–L83]
              └─ maps_to WholesalerDto [L70]
              └─ maps_to StarterPackDto [L66]
              └─ maps_to ProductSetDto [L61]
              └─ maps_to TemplateStandardAccountSetDto [L48]
              └─ uses_service UnitOfWork
                └─ method Table [L48]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/connection/{apiType}/{tenantId}/base-address  (DataGet.Api.Controllers.Connections.ConnectionController.GetBaseAddress)  [L181–L190] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetBaseAddress -> GetBaseAddressHandler [L185]
            └─ handled_by DataGet.Connections.App.Queries.GetBaseAddressHandler.Handle [L16–L30]
              └─ uses_service ConnectionContextProvider
                └─ method ConnectionContext [L27]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/master-accounts  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.GetMasterAccounts)  [L61–L70] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to MasterAccountDto [L64]
            └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
          └─ calls MasterAccountRepository.ReadQuery [L64]
          └─ query MasterAccount [L64]
            └─ reads_from MasterAccounts
          └─ uses_service MasterAccountRepository
            └─ method ReadQuery [L64]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MasterAccountRepository.ReadQuery [L8-L41]
        └─ [web] GET /api/workpaperitems/byuser/count  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetCountForUser)  [L85–L93] status=200
          └─ uses_service UserService
            └─ method GetUserId [L90]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L89]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/matching-rule-sets/{id}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Get)  [L34–L41] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to MatchingRuleSetDto [L37]
            └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
          └─ calls MatchingRuleSetRepository.ReadQuery [L37]
          └─ query MatchingRuleSet [L37]
            └─ reads_from MatchingRuleSets
        └─ [web] GET /api/super/tenants/{id}/subscriptions  (Dataverse.Api.Controllers.Super.TenantController.GetSubscriptions)  [L112–L120] status=200 [auth=Authentication.MasterPolicy]
          └─ maps_to SubscriptionDto [L115]
          └─ calls TenantRepository.ReadTable [L115]
          └─ query Tenant [L115]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L115]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/ui/fyi/entities  (Dataverse.Api.Controllers.UI.FyiController.GetEntities)  [L142–L148] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetEntitiesAsync [L145]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetEntitiesAsync [L19-L291]
        └─ [web] GET /api/accounting/reports/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.LoadReportContent)  [L347–L356] status=200 [auth=user]
          └─ calls ReportContentRepository.LoadWriteProperties [L349]
          └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
            └─ method LoadWriteProperties [L349]
              └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
        └─ [web] GET /api/binder-roll-over/stored-binder  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetBinder)  [L64–L74] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service StorageService
            └─ method GetStream [L70]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.GetStream [L17-L282]
          └─ uses_service TenantService
            └─ method GetCurrentTenant [L67]
              └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
          └─ uses_storage StorageService.GetStream [L70]
        └─ [web] GET /api/binder-data/tax  (Workpapers.Next.API.Controllers.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L34] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetTaxDataFromBinderQuery -> GetTaxDataFromBinderQueryHandler [L31]
        └─ [web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.Get)  [L34–L42] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetAutomationRunQuery -> GetAutomationRunQueryHandler [L41]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationRunQueryHandler.Handle [L49–L118]
              └─ maps_to AutomationRunStageDto [L89]
                └─ converts_to AutomationRunStageModel [L961]
              └─ maps_to AutomationRunLightDto [L85]
                └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunLightDto) [L914]
              └─ maps_to AutomationRunDto [L70]
                └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunDto) [L920]
                └─ converts_to AutomationRunUpdateModel [L960]
              └─ uses_service BotService
                └─ method GetStageDefinitions [L107]
                  └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetStageDefinitions (see previous expansion)
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L74]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service IControlledRepository<AutomationRun> (Scoped (inferred))
                └─ method ReadQuery [L70]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.AutomationRunRepository.ReadQuery
          └─ sends_request GetAutomationRunLightQuery [L40]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L37]
        └─ [web] GET /api/ui/visualisations/heatmap/find/office  (Dataverse.Api.Controllers.UI.Visualisations.HeatMapController.FindOfficeHeatMap)  [L46–L50] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/ui/workflow/task-templates/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaskTemplateDto [L48]
            └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
          └─ calls TaskTemplateRepository.ReadQuery [L48]
          └─ query TaskTemplate [L48]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/ui/users/intercom-profile  (Dataverse.Api.Controllers.UI.Core.UsersController.IntercomProfile)  [L95–L123] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IntercomService
            └─ method GetContactByExternalId [L103]
              └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserAsync [L98]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
        └─ [web] GET /api/ui/clients/{id}/children/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClientsSummary)  [L179–L207] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ClientSummaryDto [L182]
            └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryDto) [L189]
          └─ uses_client ClientRepository [L182]
          └─ calls DocumentRepository.ReadQuery [L197]
          └─ query Document [L197]
            └─ reads_from Documents
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L184]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L184]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/internal/messages/  (Dataverse.Api.Controllers.Internal.MessagesController.GetMessages)  [L32–L41] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls MessageRepository.ReadQuery [L35]
          └─ query Message [L35]
            └─ reads_from Messages
        └─ [web] GET /audit  (Dataverse.Api.Controllers.External.OfficesController.GetAllAudits)  [L39–L44] status=200
          └─ calls OfficeRepository.ReadQuery [L43]
          └─ query Office [L43]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L43]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /documents/v1/documents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.Get)  [L55–L60] status=200
          └─ maps_to DocumentDto [L58]
            └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
          └─ calls DocumentRepository.ReadQuery [L58]
          └─ query Document [L58]
            └─ reads_from Documents
        └─ [web] GET /api/conversations/{id:guid}  (Workpapers.Next.API.Controllers.ConversationsController.Get)  [L34–L46] status=200
          └─ maps_to ConversationDto [L37]
            └─ automapper.registration WorkpapersMappingProfile (Conversation->ConversationDto) [L394]
          └─ calls ConversationRepository.ReadQuery [L37]
          └─ query Conversation [L37]
            └─ reads_from Conversations
          └─ uses_service UserService
            └─ method GetUserId [L42]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/accounting/reports/masters/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAll)  [L97–L119] status=200 [auth=user]
          └─ maps_to ReportMasterLightDto [L113]
            └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterLightDto) [L571]
          └─ calls ReportMasterRepository.ReadQuery [L113]
          └─ calls EntityRepository.ReadQuery [L107]
          └─ calls FileRepository.ReadQuery [L100]
          └─ query ReportMaster [L113]
            └─ reads_from ReportMasters
          └─ query Entity [L107]
          └─ query File [L100]
            └─ reads_from Files
        └─ [web] GET /api/ui/configurations/intercom-sync/{userEmail}  (Dataverse.Api.Controllers.UI.Configuration.ConfigurationController.IsUserAllowedForIntercomSync)  [L25–L29] status=200 [auth=Authentication.MasterPolicy]
        └─ [web] GET /api/loan-matrices/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForBinderId)  [L58–L62] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetLoanMatrixForBinderQuery -> GetLoanMatrixForBinderQueryHandler [L61]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixForBinderQueryHandler.Handle [L33–L102]
              └─ calls ClientRepository.ReadQuery [L90]
              └─ maps_to LoanMatrixDto [L69]
                └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
              └─ uses_client ClientRepository [L90]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L92]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L92]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
                └─ method ReadQuery [L69]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
        └─ [web] GET /api/imanage/standard-user-authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrlForStandardUser)  [L67–L92] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
            └─ method GetApiIntegrationConfig [L73]
              └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetApiIntegrationConfig (see previous expansion)
          └─ uses_service IConnectionService (InstancePerLifetimeScope)
            └─ method GetCurrentUsersConnection [L70]
              └─ implementation Workpapers.Next.Services.ConnectionService.GetCurrentUsersConnection [L24-L211]
                └─ uses_service ApiService (SingleInstance)
                  └─ method GetAccountingFiles [L81]
                    └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
          └─ sends_request CreateOrUpdateIntegrationConfigCommand -> CreateOrUpdateIntegrationConfigCommandHandler [L85]
            └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
              └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
                └─ method WriteQuery [L36]
                  └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.WriteQuery [L5-L35]
          └─ sends_request GetIManageAuthorizationUrlQuery -> GetAuthorizationUrlQueryHandler [L82]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
              └─ uses_service IManageService
                └─ method GetAuthorisationUrl [L38]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetAuthorisationUrl [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/workpapers/auto-reconcile/wages  (Cirrus.Api.Controllers.Workpapers.AutoReconcileController.GetWages)  [L53–L59] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetApiMethods [L57]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/accounting/sourcedata/sources/types  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetSourceTypes)  [L109–L122] status=200 [auth=user]
          └─ maps_to SourceTypeDto [L112]
            └─ automapper.registration CirrusMappingProfile (SourceType->SourceTypeDto) [L205]
          └─ calls SourceTypesRepository.ReadQuery [L112]
          └─ query SourceType [L112]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetFeatures [L118]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
        └─ [web] GET /api/accounting/ledger/standard-charts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetAll)  [L63–L72] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardChartDto [L66]
            └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
            └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
          └─ calls StandardChartRepository.ReadQuery [L66]
          └─ query StandardChart [L66]
            └─ reads_from StandardCharts
        └─ [web] GET /api/stats/productusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.ProductUsage)  [L101–L114] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetProductUsageQuery -> GetProductUsageQueryHandler [L111]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetProductUsageQueryHandler.Handle [L47–L133]
              └─ uses_client KeenClient [L96]
              └─ uses_service UnitOfWork
                └─ method Table [L102]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service KeenQueryBuilder
                └─ method Build [L97]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L96]
                  └─ ... (no implementation details available)
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=ProductUsage-period{*}-firmid{*}) [L67]
        └─ [web] GET /workflow/v1/deliverables/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.Get)  [L52–L57] status=200
          └─ maps_to DeliverableDto [L55]
            └─ automapper.registration ExternalApiMappingProfile (Deliverable->DeliverableDto) [L130]
            └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableDto) [L353]
          └─ calls DeliverableRepository.ReadQuery [L55]
          └─ query Deliverable [L55]
            └─ reads_from Deliverables
        └─ [web] GET /api/internal/task-template-groups  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplateGroupsController.GetAll)  [L34–L43] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TaskTemplateGroupDto [L37]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
            └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
          └─ calls TaskTemplateGroupRepository.ReadQuery [L37]
          └─ query TaskTemplateGroup [L37]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/matter-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.Get)  [L58–L65] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to MatterTemplateDto [L61]
            └─ automapper.registration WorkpapersMappingProfile (MatterTemplate->MatterTemplateDto) [L790]
          └─ calls MatterTemplateRepository.ReadQuery [L61]
          └─ query MatterTemplate [L61]
            └─ reads_from MatterTemplates
        └─ [web] GET /api/companies-house/company/{companyNumber}/insolvency  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyInsolvency)  [L203–L211] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyInsolvencyQuery -> GetCompanyInsolvencyQueryHandler [L208]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyInsolvencyQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/sources/export/types  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetSourceTypes)  [L54–L59] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetSourceTypesQuery -> GetSourceTypesQueryHandler [L58]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.SourceTypes.GetSourceTypesQueryHandler.Handle [L33–L62]
              └─ calls SourceTypesRepository.ReadQuery [L46]
              └─ maps_to SourceTypeLightDto [L58]
        └─ [web] GET /api/ui/teams/{id}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Get)  [L51–L57] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TeamDto [L54]
            └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
            └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
          └─ calls TeamRepository.ReadQuery [L54]
          └─ query Team [L54]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L54]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/users/{id}  (Dataverse.Api.Controllers.Internal.Core.UsersController.Get)  [L92–L98] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserDto [L95]
            └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
            └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
          └─ calls UserRepository.ReadQuery [L95]
          └─ query User [L95]
          └─ uses_service UserRepository
            └─ method ReadQuery [L95]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/worksheets/templatewithdownload  (Workpapers.Next.API.Controllers.WorksheetsController.TemplateWithDownload)  [L104–L122] status=200
          └─ maps_to WorkpaperRecordTemplateV2Dto [L118]
            └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
            └─ converts_to LegacyDocumentDto [L343]
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L119]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L118]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetUser [L107]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L119]
          └─ sends_request GetTemplateForDateTimeQuery -> GetTemplateForDateTimeQueryHandler [L107]
        └─ [web] GET /api/stats/starterusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.StarterUsage)  [L159–L172] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetStarterUsageQuery -> GetStarterUsageQueryHandler [L169]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetStarterUsageQueryHandler.Handle [L47–L128]
              └─ uses_client KeenClient [L97]
              └─ uses_service UnitOfWork
                └─ method Table [L106]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service KeenQueryBuilder
                └─ method Build [L98]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L97]
                  └─ ... (no implementation details available)
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=StarterUsage-period{*}-firmid{*}) [L68]
        └─ [web] GET /api/template-versions/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.GetVersion)  [L48–L58] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
          └─ maps_to TemplateVersionDto [L52]
          └─ uses_service UnitOfWork
            └─ method Table [L52]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/fyi/verify-connection  (DataGet.Api.Controllers.Integrations.FyiController.VerifyConnection)  [L304–L317] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service UserTokenService
            └─ method GetTokenAsync [L309]
              └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync (see previous expansion)
        └─ [web] GET /api/loan-matrices/for-client-group/{clientGroupId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForClientGroup)  [L47–L51] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetLoanMatrixByClientGroupQuery -> GetLoanMatrixByClientGroupQueryHandler [L50]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixByClientGroupQueryHandler.Handle [L36–L94]
              └─ calls ClientRepository.ReadQuery [L82]
              └─ maps_to LoanMatrixDto [L63]
                └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
              └─ uses_client ClientRepository [L82]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L84]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L84]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
                └─ method ReadQuery [L63]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
        └─ [web] GET /workpapers/v1/binder-types/{binderTypeId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.Get)  [L42–L48] status=200
          └─ maps_to BinderTypeDto [L45]
            └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
            └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
          └─ calls BinderTypeRepository.ReadQuery [L45]
          └─ query BinderType [L45]
            └─ reads_from BinderTypes
        └─ [web] GET /api/external/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetById)  [L72–L79] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to SyncConfigurationInsecureDto [L75]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
          └─ calls SyncConfigurationRepository.ReadQuery [L75]
          └─ query SyncConfiguration [L75]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/internal/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.GetById)  [L65–L73] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to DeliverableTypeDto [L68]
            └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
            └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
          └─ calls DeliverableTypeRepository.ReadQuery [L68]
          └─ query DeliverableType [L68]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.GetSupportUsersAsync)  [L26–L34] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetSupportUsersQuery -> GetSupportUsersQueryHandler [L32]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetSupportUsersQueryHandler.Handle [L29–L61]
              └─ calls TenantRepository.ReadTable [L49]
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L47]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /api/team-users/for-team/{teamId:Guid}  (Cirrus.Api.Controllers.Firm.TeamUsersController.GetAllForTeam)  [L57–L68] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TeamUserWithUserDto [L62]
            └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserWithUserDto) [L141]
          └─ calls TeamUserRepository.ReadQuery [L62]
          └─ query TeamUser [L62]
            └─ reads_from TeamUsers
        └─ [web] GET /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetAllForDataset)  [L55–L65] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowJournalLightDto [L59]
            └─ automapper.registration CirrusMappingProfile (CashflowJournal->CashflowJournalLightDto) [L509]
          └─ calls CashflowJournalRepository.ReadQuery [L59]
          └─ query CashflowJournal [L59]
            └─ reads_from CashflowJournals
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L58]
        └─ [web] GET /api/ui/workflow/deliverables/default-name  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetJobDefaultName)  [L74–L83] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request DefaultDeliverableNameQuery -> DefaultDeliverableNameQueryHandler [L81]
            └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultDeliverableNameQueryHandler.Handle [L41–L136]
              └─ maps_to DeliverableTypeDto [L94]
              └─ uses_service JobParamsService
                └─ method GetClient [L93]
                  └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient (see previous expansion)
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L80]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L80]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<Job> (Scoped (inferred))
                └─ method ReadQuery [L75]
                  └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L74]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
          └─ sends_request DefaultDeliverableNameQuery -> DefaultDeliverableNameQueryHandler [L80]
        └─ [web] GET /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Get)  [L90–L110] status=200 [auth=Authentication.MasterPolicy]
          └─ maps_to TenantDto [L93]
          └─ calls TenantRepository.ReadTable [L93]
          └─ query Tenant [L93]
            └─ reads_from Tenants
          └─ uses_service UserService
            └─ method GetIdentityId [L103]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId (see previous expansion)
          └─ uses_service TenantRepository
            └─ method ReadTable [L93]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/accounting/reports/styles/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.GetAll)  [L59–L66] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ReportStyleLightDto [L62]
            └─ automapper.registration CirrusMappingProfile (ReportStyle->ReportStyleLightDto) [L582]
          └─ calls ReportStyleRepository.ReadQuery [L62]
          └─ query ReportStyle [L62]
            └─ reads_from ReportStyles
        └─ [web] GET /api/ato/gov-link/client-account-transactions/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshClientAccountTransactionSummary)  [L149–L160] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetClientAccountTransactionSummaryQuery -> GetClientAccountTransactionSummaryQueryHandler [L156]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetClientAccountTransactionSummaryQueryHandler.Handle [L35–L70]
              └─ uses_client DataGetClient [L64]
                └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetClientAccountSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/client-accounts/summary  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionSummary)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetClientAccountSummaryAsync [L64]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetClientAccountSummaryAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L54]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAll)  [L50–L58] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to BinderFieldDto [L54]
            └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
          └─ calls BinderFieldRepository.ReadQuery [L54]
          └─ query BinderField [L54]
            └─ reads_from BinderFields
        └─ [web] GET /workpapers/v1/binders/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderMinimalQuery)  [L62–L69] status=200
          └─ calls BinderRepository.ReadQuery [L65]
          └─ query Binder [L65]
            └─ reads_from Binders
        └─ [web] GET /api/ui/visualisations/friction-map/find  (Dataverse.Api.Controllers.UI.Visualisations.FrictionMapController.FindFrictionMap)  [L24–L28] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/companies-house/search/disqualified-officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDisqualifiedOfficers)  [L60–L68] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchDisqualifiedOfficersQuery -> SearchDisqualifiedOfficersQueryHandler [L65]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDisqualifiedOfficersQueryHandler.Handle [L21–L32]
        └─ [web] GET /api/sources/{type}/debtors  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetDebtors)  [L312–L339] status=200 [auth=AuthorizationPolicies.User]
          └─ calls WorkpaperRecordRepository.ReadQuery [L330]
          └─ query WorkpaperRecord [L330]
            └─ reads_from WorkpaperRecords
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L336]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L317]
        └─ [web] GET /api/accounting/ledger/cashflow/lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.GetLine)  [L42–L51] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowLineDto [L50]
          └─ calls CashflowLineRepository.ReadQuery [L45]
          └─ query CashflowLine [L45]
            └─ reads_from CashflowLines
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L48]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/preview  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.Preview)  [L262–L270] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetWorksheetPdfQuery -> GetWorksheetPdfQueryHandler [L267]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.GetWorksheetPdfQueryHandler.Handle [L37–L76]
              └─ maps_to WorksheetUltraLightDto [L63]
                └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetUltraLightDto) [L478]
              └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
                └─ method ReadQuery [L63]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L54]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L265]
        └─ [web] GET /workpapers/v1/matters/{matterId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.Get)  [L43–L49] status=200
          └─ maps_to MatterDto [L46]
            └─ automapper.registration WorkpapersMappingProfile (Matter->MatterDto) [L781]
            └─ automapper.registration ExternalApiMappingProfile (Matter->MatterDto) [L206]
          └─ calls MatterRepository.ReadQuery [L46]
          └─ query Matter [L46]
            └─ reads_from Matters
        └─ [web] GET /workpapers/v1/custom-statuses/{statusId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.Get)  [L42–L48] status=200
          └─ maps_to CustomStatusDto [L45]
            └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
            └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
          └─ calls CustomStatusRepository.ReadQuery [L45]
          └─ query CustomStatus [L45]
            └─ reads_from CustomStatuses
        └─ [web] GET /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Get)  [L46–L52] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to ContactDto [L49]
            └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
          └─ calls ContactRepository.ReadQuery [L49]
          └─ query Contact [L49]
            └─ reads_from DVS_Clients
        └─ [web] GET /api/accounting/ledger/auto-journals/distributions/{datasetId:Guid}/profit  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.DistributionController.GetProfit)  [L65–L72] status=200 [auth=user]
          └─ calls DatasetRepository.ReadQuery [L68]
          └─ query Dataset [L68]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L69]
        └─ [web] GET /api/accounting/ledger/standard-accounts/child/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetChild)  [L96–L103] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardChildDto [L99]
          └─ calls StandardAccountRepository.ReadQuery [L99]
          └─ query StandardAccount [L99]
            └─ reads_from StandardAccounts
        └─ [web] GET /api/sources/{type}/ledger  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedger)  [L341–L402] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetGeneralLedgerBySourceAccountQuery -> GetGeneralLedgerBySourceAccountQueryHandler [L379]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L356]
        └─ [web] GET /api/accounting/assets/asset-groups  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.MapSourceAccounts)  [L152–L172] status=200 [auth=user]
          └─ sends_request MapSourceAccountsCommand -> MapSourceAccountsCommandHandler [L170]
            └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
              └─ calls SourceAccountsCachedRepository (methods: AddTrackedRange,InDatabaseOnly,InMemoryOnly) [L92]
              └─ uses_service IControlledRepository<Source> (Scoped (inferred))
                └─ method WriteQuery [L151]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.WriteQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L123]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/connection/{apiType}/tenants  (DataGet.Api.Controllers.Connections.ConnectionController.GetTenants)  [L159–L168] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/accounting/ledger/reports/{datasetId}/accounts  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetLedgerForAccounts)  [L101–L122] status=200 [auth=user]
          └─ sends_request GetGeneralLedgerByAccountQuery -> GetGeneralLedgerByAccountQueryHandler [L120]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerByAccountQueryHandler.Handle [L56–L240]
              └─ maps_to SourceAccountMappingDto [L193]
              └─ maps_to SourceLightDto [L176]
              └─ maps_to AccountWithTransactionsDto [L82]
                └─ automapper.registration CirrusReportMappingProfile (Account->AccountWithTransactionsDto) [L133]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L201]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service ApiService (SingleInstance)
                └─ method GetFeatures [L181]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method Execute [L144]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L129]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service GetGeneralLedgerForDatesQuery
                └─ method Execute [L89]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQuery.Execute
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L82]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L119]
        └─ [web] GET /api/fyi/categories/{categoryId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCategory)  [L78–L87] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCategoryQuery -> GetCategoryQueryHandler [L83]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoryQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetCategory [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCategory [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}/from-api  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSourceFromApi)  [L139–L143] status=200 [auth=user]
          └─ sends_request GetAccountsFromApiQuery -> ImportFromApiCommandHandler [L142]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.ImportFromApiCommandHandler.Handle [L34–L61]
              └─ uses_service ApiService (SingleInstance)
                └─ method GetApiMethods [L56]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L54]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Source> (Scoped (inferred))
                └─ method ReadQuery [L51]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.ReadQuery
        └─ [web] GET /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Get)  [L49–L73] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to BinderRecordTemplateDto [L59]
          └─ maps_to BinderTemplateDto [L55]
            └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
          └─ calls ExcludedBinderTemplateRepository.ReadQuery [L70]
          └─ calls BinderTemplateRepository.ReadQuery [L55]
          └─ query ExcludedBinderTemplate [L70]
            └─ reads_from ExcludedBinderTemplates
          └─ query BinderTemplate [L55]
            └─ reads_from BinderTemplates
          └─ uses_service UserService
            └─ method IsSuperUser [L53]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
        └─ [web] GET /api/fyi/authentication-fyi-elite  (DataGet.Api.Controllers.Integrations.FyiController.GetAccessInfoWithAccessSecretForFyiElite)  [L270–L281] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service UserTokenService
            └─ method GetTokenAsync [L275]
              └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync (see previous expansion)
        └─ [web] GET /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetAll)  [L53–L63] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to JobTypeDto [L56]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
            └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
          └─ calls JobTypeRepository.ReadQuery [L56]
          └─ query JobType [L56]
            └─ reads_from JobTypes
        └─ [web] GET /api/accounting/ledger/source-accounts/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Get)  [L52–L57] status=200 [auth=user]
          └─ maps_to SourceAccountDto [L55]
            └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountDto) [L256]
          └─ calls SourceAccountRepository.ReadQuery [L55]
          └─ query SourceAccount [L55]
            └─ reads_from SourceAccounts
        └─ [web] GET /api/ui/templates/{id}/preview  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.PreviewTemplate)  [L76–L84] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ITemplateService (AddSingleton)
            └─ method GetTemplatePreviewUrl [L81]
              └─ implementation Dataverse.Templates.TemplateService.GetTemplatePreviewUrl [L17-L359]
                └─ uses_service TemplateConfiguration
                  └─ method GetSiteDriveId [L333]
                    └─ ... (no implementation details available)
                └─ uses_service SharePointTemplateManager
                  └─ method GetDownloadUrl [L82]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Get)  [L35–L42] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ maps_to ReportWatermarkDto [L38]
            └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
          └─ calls WatermarkRepository.ReadQuery [L38]
          └─ query ReportWatermark [L38]
            └─ reads_from ReportWatermarks
        └─ [web] GET /api/connections/xero/bas  (Workpapers.Next.API.Controllers.Connections.XeroController.GetBAS)  [L78–L84] status=200
        └─ [web] GET /workflow/v1/kanban-columns/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.FullQuery)  [L83–L89] status=200
          └─ calls KanbanColumnRepository.ReadQuery [L87]
          └─ query KanbanColumn [L87]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/accounting/assets/reports/full-summary/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetFullSummaryReportExcel)  [L80–L86] status=200 [auth=user]
          └─ sends_request GetExportAssetReportQuery -> GetExportAssetReportQueryHandler [L84]
        └─ [web] GET /api/ui/integration-settings/  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.GetAll)  [L37–L43] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to IntegrationSettingsDto [L40]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L40]
          └─ query IntegrationSettings [L40]
            └─ reads_from IntegrationSettingss
        └─ [web] GET /api/ui/account-mapping/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.GetMappingCodes)  [L59–L68] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/datasets/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetForFile)  [L90–L112] status=200 [auth=user]
          └─ maps_to DatasetLightDto [L100]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
          └─ calls DatasetRepository.ReadQuery [L100]
          └─ query Dataset [L100]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L99]
        └─ [web] GET /api/accounting/ledger/standard-charts/sets  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetStandardChartSets)  [L78–L84] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardChartSetDto [L81]
          └─ uses_service IReadRepository (InstancePerLifetimeScope)
            └─ method Table [L81]
              └─ ... (no implementation details available)
        └─ [web] GET /api/ui/workflow/jobs/series/batches  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeriesBatches)  [L145–L162] status=200 [auth=Authentication.UserPolicy]
          └─ calls JobRepository.ReadQuery [L151]
          └─ query Job [L151]
            └─ reads_from Jobs
        └─ [web] GET /api/ato/client-account-transactions  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetClientAccountTransactions)  [L50–L60] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetClientAccountTransactionsQuery -> GetClientAccountTransactionsQueryHandler [L56]
        └─ [web] GET /api/super/offices/{id:Guid}  (Dataverse.Api.Controllers.Super.Core.OfficesController.Get)  [L65–L70] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to OfficeDto [L68]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
            └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
          └─ calls OfficeRepository.ReadQuery [L68]
          └─ query Office [L68]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L68]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/files/for-entity/{entityId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForEntity)  [L137–L158] status=200 [auth=user]
          └─ maps_to FileLightDto [L153]
            └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
          └─ calls FileRepository.ReadQuery [L153]
          └─ calls EntityRepository.ReadQuery [L145]
          └─ query File [L153]
            └─ reads_from Files
          └─ query Entity [L145]
          └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L152]
        └─ [web] GET /ledger/v1/datasets/detailed  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.GetDatasetsDetailedQuery)  [L64–L71] status=200
          └─ calls DatasetRepository.ReadQuery [L67]
          └─ query Dataset [L67]
        └─ [web] GET /api/ui/fyi/categories/{categoryId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetCategory)  [L71–L77] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetCategoryAsync [L74]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCategoryAsync [L19-L291]
        └─ [web] GET /api/accounting/reports/masters/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAllMasterTemplates)  [L125–L129] status=200 [auth=user,admin]
          └─ sends_request GetReportMasterTemplates -> GetReportMasterTemplatesHandler [L128]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterTemplates.GetReportMasterTemplatesHandler.Handle [L23–L47]
        └─ [web] GET /api/accounting/reports/notes/disclosures/variants  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.CanIEditDisclosureVariant)  [L177–L188] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method IsInRole [L181]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L186]
        └─ [web] GET /api/connections/reportance/journal-url  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAddJournalUrl)  [L84–L90] status=200
          └─ sends_request GetCirrusAddJournalUrl -> GetCirrusJournalUrlHandler [L87]
            └─ handled_by Workpapers.Next.CirrusServices.Queries.GetCirrusJournalUrlHandler.Handle [L20–L34]
              └─ uses_service CirrusConfig
                └─ method SiteBaseUrl [L32]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/super/templates/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Templates.TemplatesSubscriptionController.GetSubscription)  [L42–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L47]
          └─ calls TenantRepository.ReadTable [L47]
          └─ query Tenant [L47]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L47]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/connection/process-callback  (DataGet.Api.Controllers.Connections.ConnectionController.ProcessCallback)  [L56–L85] status=200 [AllowAnonymous]
          └─ sends_request ProcessCallbackCommand -> ProcessCallbackCommandHandler [L74]
            └─ handled_by DataGet.Connections.App.Commands.ProcessCallbackCommandHandler.Handle [L24–L92]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L73]
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                └─ method GetExternalApiFromApiType [L69]
                  └─ ... (no implementation details available)
              └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
                └─ method AssignActiveConnectionAsync [L67]
                  └─ ... (no implementation details available)
              └─ uses_service RequestContextProvider
                └─ method AssignActiveContext [L66]
                  └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.AssignActiveContext
              └─ uses_service IConnectionService (InstancePerLifetimeScope)
                └─ method GetConnectionAsync [L48]
                  └─ implementation Workpapers.Next.Services.ConnectionService.GetConnectionAsync [L24-L211]
                    └─ uses_service ApiService (SingleInstance)
                      └─ method GetAccountingFiles [L81]
                        └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles (see previous expansion)
        └─ [web] GET /api/accounting/assets/assets/  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Get)  [L60–L92] status=200 [auth=user]
          └─ calls DepreciationRecordRepository.ReadQuery [L78]
          └─ calls AssetRepository.ReadQuery [L71]
          └─ query DepreciationRecord [L78]
            └─ reads_from DepreciationRecords
          └─ query Asset [L71]
            └─ reads_from Assets
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L69]
        └─ [web] GET /core/v1/teams/detailed  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.FullQuery)  [L85–L90] status=200
          └─ calls TeamRepository.ReadQuery [L88]
          └─ query Team [L88]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L88]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/workflow/tasks/for-deliverable/{deliverableId:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetDeliverableTasks)  [L74–L86] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to WorkflowTaskLightDto [L80]
          └─ calls TaskRepository.ReadQuery [L80]
          └─ uses_service TaskRepository
            └─ method ReadQuery [L80]
              └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery (see previous expansion)
          └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L78]
        └─ [web] GET /api/ui/workflow/job-statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.GetById)  [L57–L65] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobStatusDto [L60]
            └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
            └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
          └─ calls JobStatusRepository.ReadQuery [L60]
          └─ query JobStatus [L60]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/gov-link/activity-statements/test/detail/single  (DataGet.Api.Controllers.GovLink.ActivityStatementController.SingleActivityStatementTest)  [L56–L63] status=200 [AllowAnonymous]
          └─ uses_service TestService
            └─ method GetSingleActivityStatement [L62]
              └─ ... (no implementation details available)
        └─ [web] GET /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Get)  [L52–L59] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to BinderAutomationRuleDto [L55]
            └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
          └─ calls BinderAutomationRuleRepository.ReadQuery [L55]
          └─ query BinderAutomationRule [L55]
            └─ reads_from BinderAutomationRules
        └─ [web] GET /api/claims  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetUserInfo)  [L127–L146] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/accounting/ledger/import-runs/compare  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.GetComparison)  [L95–L99] status=200 [auth=user]
        └─ [web] GET /api/accounting/ledger/accounts/{fileId:Guid}/workpapers-info  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAccountsForWorkpapers)  [L189–L195] status=200 [auth=user]
          └─ sends_request GetAccountsForWorkpapersQuery -> GetAccountsForWorkpapersQueryHandler [L193]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountsForWorkpapersQueryHandler.Handle [L25–L74]
              └─ maps_to AccountInfoForWorkpapersDto [L40]
                └─ automapper.registration CirrusMappingProfile (Account->AccountInfoForWorkpapersDto) [L354]
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L59]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L40]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L192]
        └─ [web] GET /api/ato/gov-link/individual-income-tax-returns  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshIndividualIncomeTaxReturns)  [L162–L169] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetIndividualIncomeTaxReturnQuery -> GetIndividualIncomeTaxReturnQueryHandler [L166]
            └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnQueryHandler.Handle [L89–L120]
              └─ uses_client IAtoClient [L103]
              └─ uses_service IAtoClient (AddScoped)
                └─ method RequestAsync [L103]
                  └─ ... (no implementation details available)
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnQueryHandler.Handle [L33–L73]
              └─ uses_client DataGetClient [L60]
              └─ uses_service DataGetClient
                └─ method GetIndividualIncomeTaxReturnAsync [L60]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetIndividualIncomeTaxReturnAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L52]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
          └─ returns IndividualIncomeTaxReturnDto [L166]
        └─ [web] GET /api/binder-templates/global  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.GlobalSearch)  [L82–L87] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ sends_request GetBinderTemplatesGlobalQuery [L86]
        └─ [web] GET /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.GetBenchmarkCode)  [L32–L37] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetBenchmarkCodeQuery -> GetBenchmarkCodeQueryHandler [L36]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodes.GetBenchmarkCodeQueryHandler.Handle [L28–L47]
              └─ maps_to BenchmarkCodeDto [L41]
                └─ automapper.registration DataverseMappingProfile (BenchmarkCode->BenchmarkCodeDto) [L245]
              └─ uses_service IControlledRepository<BenchmarkCode> (Scoped (inferred))
                └─ method ReadQuery [L41]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeRepository.ReadQuery
        └─ [web] GET /api/workpaper-record-templates/{id:Guid}/template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetTemplate)  [L78–L82] status=200
          └─ sends_request GetWorkpaperRecordTemplateToInsertQuery -> GetWorkpaperRecordTemplateToInsertQueryHandler [L81]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateToInsertQueryHandler.Handle [L36–L71]
              └─ maps_to TemplateLightDto [L69]
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L57]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
        └─ [web] GET /api/accounting/ledger/accounttypes/search  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.Search)  [L53–L57] status=200 [auth=user]
        └─ [web] GET /api/accounting/reports/styles/excel/download/{reportStyleId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportExcelController.DownloadTemplate)  [L55–L79] status=200 [auth=Authentication.AdminPolicy]
          └─ calls ReportStyleRepository.ReadQuery [L59]
          └─ query ReportStyle [L59]
            └─ reads_from ReportStyles
        └─ [web] GET /api/accounting/ledger/accounts/search/master  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Search)  [L148–L152] status=200 [auth=user]
        └─ [web] GET /api/accounting-file/{fileId}/legacy-clientdetails  (DataGet.Api.Controllers.Connections.AccountingFileController.LegacyGetClientDetails)  [L147–L155] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/template-versions/for-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.GetVersions)  [L60–L72] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
          └─ maps_to TemplateVersionDto [L66]
          └─ uses_service UnitOfWork
            └─ method Table [L66]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/account-mapping/external-reporting-systems/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCodesByCode)  [L108–L114] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodesByCodeQuery -> GetExternalReportingSystemMappingCodesByCodeQueryHandler [L112]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesByCodeQueryHandler.Handle [L32–L61]
              └─ maps_to ExternalReportingSystemMappingCodeDto [L49]
                └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
              └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
                └─ method ReadQuery [L49]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
        └─ [web] GET /api/accounting/taxforms/  (Cirrus.Api.Controllers.Accounting.TaxFormsController.GetForms)  [L30–L34] status=200 [auth=user]
        └─ [web] GET /api/ui/visualisations/heatmap/find/user  (Dataverse.Api.Controllers.UI.Visualisations.HeatMapController.FindUserHeatMap)  [L28–L32] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/ui/workflow/deliverables/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetDeliverable)  [L64–L72] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DeliverableDto [L68]
          └─ calls DeliverableRepository.ReadQuery [L68]
          └─ query Deliverable [L68]
            └─ reads_from Deliverables
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L70]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L70]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service DeliverableRepository
            └─ method ReadQuery [L68]
              └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery [L8-L45]
        └─ [web] GET /api/accounting/reports/masters/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Get)  [L62–L66] status=200 [auth=user,admin]
          └─ sends_request GetReportMasterQuery -> GetReportMasterQueryHandler [L65]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportMasterQueryHandler.Handle [L28–L73]
              └─ maps_to ReportMasterDepreciationPageDetailDto [L54]
              └─ maps_to ReportMasterFinancialPageDetailDto [L46]
              └─ maps_to ReportMasterDto [L42]
                └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterDto) [L570]
              └─ uses_service IControlledRepository<ReportMaster> (Scoped (inferred))
                └─ method ReadQuery [L42]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportMasterRepository.ReadQuery
        └─ [web] GET /api/internal/documents  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocumentModelFromBinder)  [L336–L376] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/internal/documents/workpapers-creation-defaults  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetWorkpapersDocumentCreationDefaults)  [L159–L186] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls DocumentStatusRepository.ReadQuery [L173]
          └─ calls CabinetRepository.ReadQuery [L162]
          └─ query DocumentStatus [L173]
            └─ reads_from DVS_DocumentStatuses
          └─ query Cabinet [L162]
            └─ reads_from Cabinets
        └─ [web] GET /api/ui/documents/documents/{id}/edit  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.EditDocument)  [L292–L299] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request EditDocumentCommand -> EditDocumentCommandHandler [L296]
            └─ handled_by Dataverse.ApplicationService.Commands.Documents.EditDocumentCommandHandler.Handle [L41–L125]
              └─ uses_service UserService
                └─ method GetUserId [L111]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<DocumentAuditLog> (Scoped (inferred))
                └─ method Add [L106]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentAuditLogRepository.Add
              └─ uses_service DocumentServiceFactory
                └─ method GetDocumentWithService [L83]
                  └─ implementation DocumentServiceFactory.GetDocumentWithService
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L82]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method WriteQuery [L71]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L295]
        └─ [web] GET /api/journals/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.Get)  [L66–L98] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to JournalDto [L91]
          └─ calls BinderRepository.ReadQuery [L75]
          └─ query Binder [L75]
            └─ reads_from Binders
          └─ uses_service Service
            └─ method Get [L84]
              └─ ... (no implementation details available)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L94]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L73]
        └─ [web] GET /api/companies-house/search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompanies)  [L39–L48] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchCompaniesQuery -> SearchCompaniesQueryHandler [L45]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesQueryHandler.Handle [L23–L34]
        └─ [web] GET /api/accounting/files/for-client/{clientId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForClient)  [L103–L128] status=200 [auth=user]
          └─ maps_to FileLightDto [L122]
            └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
          └─ calls FileRepository.ReadQuery [L122]
          └─ calls ClientRepository.ReadQuery [L112]
          └─ query File [L122]
            └─ reads_from Files
          └─ query Client [L112]
        └─ [web] GET /api/external/sync-configuration/  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetAll)  [L55–L70] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to SyncConfigurationInsecureDto [L58]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
          └─ uses_cache IDistributedCache.SetRecordAsync [write] [L67]
          └─ calls SyncConfigurationRepository.ReadQuery [L58]
          └─ query SyncConfiguration [L58]
            └─ reads_from SyncConfigurations
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L66]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /api/accounting/ledger/accounts/header/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderAccount)  [L127–L131] status=200 [auth=user]
          └─ sends_request GetHeaderAccountQuery -> GetAccountQueryHandler [L130]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountQueryHandler.Handle [L37–L82]
              └─ maps_to ChildAccountDto [L66]
              └─ maps_to HeaderAccountDto [L55]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L77]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
        └─ [web] GET /api/proposed-items/automation-bots  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetProposedAutomatonBots)  [L79–L96] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ProposedAutomationRunDto [L86]
            └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedAutomationRunDto) [L974]
          └─ calls ProposedItemRepository.ReadQuery [L86]
          └─ calls BinderRepository.ReadQuery [L83]
          └─ query ProposedItem [L86]
            └─ reads_from ProposedItems
          └─ query Binder [L83]
            └─ reads_from Binders
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L82]
        └─ [web] GET /core/v1/tax-agents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.Get)  [L41–L46] status=200
          └─ maps_to TaxAgentDto [L44]
            └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
            └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
          └─ calls TaxAgentRepository.ReadQuery [L44]
          └─ query TaxAgent [L44]
            └─ reads_from TaxAgents
        └─ [web] GET /api/super/teams/{id}  (Dataverse.Api.Controllers.Super.Core.TeamsController.Get)  [L38–L43] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TeamDto [L41]
            └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
            └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
          └─ calls TeamRepository.ReadQuery [L41]
          └─ query Team [L41]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L41]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /audit  (Dataverse.Api.Controllers.External.UsersController.GetAllAudits)  [L40–L45] status=200
          └─ calls UserRepository.ReadQuery [L44]
          └─ query User [L44]
          └─ uses_service UserRepository
            └─ method ReadQuery [L44]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/super/support-users/active-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.GetActiveSupportUsers)  [L57–L62] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/health  (Dataverse.Api.Controllers.HealthController.Alive)  [L10–L14] status=200 [AllowAnonymous]
        └─ [web] GET /api/sources/{type}/bas  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBas)  [L237–L258] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L255]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L242]
        └─ [web] GET /api/workpaperitems  (Workpapers.Next.API.Controllers.WorkpaperItemsController.CheckIsAuthorized)  [L268–L278] status=200
          └─ uses_service UserService
            └─ method GetFirmId [L272]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L270]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/internal/deliverables/{id}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverablesController.Get)  [L53–L59] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to DeliverableDto [L56]
            └─ automapper.registration ExternalApiMappingProfile (Deliverable->DeliverableDto) [L130]
            └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableDto) [L353]
          └─ calls DeliverableRepository.ReadQuery [L56]
          └─ query Deliverable [L56]
            └─ reads_from Deliverables
        └─ [web] GET /api/fyi/test-connection  (DataGet.Api.Controllers.Integrations.FyiController.TestConnection)  [L293–L302] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/versions  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentVersions)  [L280–L288] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageDocumentVersionsQuery -> GetIManageDocumentVersionsQueryHandler [L287]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentVersionsQueryHandler.Handle [L20–L32]
              └─ uses_service IManageService
                └─ method GetVersions [L29]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetVersions [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/ui/connections/has-connection  (Dataverse.Api.Controllers.UI.ConnectionsController.HasConnection)  [L42–L58] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetConnectionSummaryQuery -> GetConnectionSummaryQueryHandler [L47]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetConnectionSummaryQueryHandler.Handle [L22–L93]
              └─ maps_to ActiveConnectionDto [L55]
                └─ automapper.registration CirrusMappingProfile (SourceType->ActiveConnectionDto) [L208]
              └─ uses_client DataGetClient [L64]
              └─ uses_service ApiService (SingleInstance)
                └─ method GetFeatures [L69]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetConnectionsAsync [L64]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetConnectionsAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service IControlledRepository<UserToken> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Accounting.Connections.UserTokenRepository.ReadQuery
              └─ uses_service IControlledRepository<FileToken> (Scoped (inferred))
                └─ method ReadQuery [L61]
                  └─ implementation Cirrus.Data.Repository.Accounting.Connections.FileTokenRepository.ReadQuery
              └─ uses_service IControlledRepository<SourceType> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceTypesRepository.ReadQuery [L7-L39]
              └─ uses_service UserService
                └─ method GetUserId [L54]
                  └─ implementation Cirrus.ApplicationService.Services.UserService.GetUserId [L14-L122]
                    └─ uses_service IControlledRepository<User> (Scoped (inferred))
                      └─ method GetUserId [L50]
                        └─ implementation Cirrus.Data.Repository.Firm.UserRepository.GetUserId
                    └─ uses_service User
                      └─ method GetUserId [L37]
                        └─ implementation Cirrus.DomainModel.Model.Firm.User.GetUserId (see previous expansion)
                    └─ uses_service Guid?
                      └─ method GetUserId [L34]
                        └─ ... (no implementation details available)
        └─ [web] GET /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DeliverableTypeLightDto [L45]
            └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L359]
          └─ calls DeliverableTypeRepository.ReadQuery [L45]
          └─ query DeliverableType [L45]
            └─ reads_from DeliverableTypes
        └─ [web] GET /audit  (Dataverse.Api.Controllers.External.ClientsController.GetAllAudits)  [L39–L44] status=200
          └─ uses_client ClientRepository [L43]
        └─ [web] GET /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAccounts)  [L60–L65] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetSourceAccountsByIdQuery -> GetSourceAccountsByIdQueryHandler [L64]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsByIdQueryHandler.Handle [L45–L95]
              └─ calls SourceAccountRepository.ReadQuery [L85]
              └─ maps_to SourceAccountDto [L93]
              └─ uses_service SourceAccountsQueryProcessor
                └─ method ProcessSourceAccounts [L67]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/super/offices/  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetAll)  [L56–L63] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to OfficeLightDto [L59]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
          └─ calls OfficeRepository.ReadQuery [L59]
          └─ query Office [L59]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L59]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/offices  (Workpapers.Next.API.Controllers.OfficeController.GetOffices)  [L58–L71] status=200
          └─ maps_to OfficeLightDto [L63]
          └─ uses_service UnitOfWork
            └─ method Table [L63]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L61]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
        └─ [web] GET /api/teams/{id:Guid}/users  (Workpapers.Next.API.Controllers.TeamController.GetTeamUsers)  [L82–L92] status=200
          └─ maps_to UserDto [L87]
          └─ uses_service UnitOfWork
            └─ method Table [L87]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/firm/settings/  (Dataverse.Api.Controllers.UI.Firm.FirmSettingsController.Get)  [L38–L44] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to FirmSettingsDto [L41]
            └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
          └─ calls FirmSettingsRepository.ReadQuery [L41]
          └─ query FirmSettings [L41]
            └─ reads_from FirmSettingss
        └─ [web] GET /api/workpapers/byfirm  (Workpapers.Next.API.Controllers.WorkpapersController.GetForFirm)  [L57–L61] status=200
        └─ [web] GET /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Search)  [L86–L109] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindClientsQuery -> FindClientsQueryHandler [L108]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsQueryHandler.Handle [L76–L187]
              └─ uses_service IControlledRepository<Office> (Scoped (inferred))
                └─ method ReadQuery [L115]
                  └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L108]
                  └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service IUserService (InstancePerLifetimeScope)
                └─ method GetUserId [L99]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service IControlledRepository<Client> (Scoped (inferred))
                └─ method ReadQuery [L96]
                  └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
          └─ sends_request FindClientsLightQuery [L107]
        └─ [web] GET /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to KanbanColumnDto [L48]
            └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
            └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
          └─ calls KanbanColumnRepository.ReadQuery [L48]
          └─ query KanbanColumn [L48]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/internal/Propagator/subscriptions  (Dataverse.Api.Controllers.Internal.PropagatorController.GetTenantSubscriptions)  [L122–L128] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionLightDto [L125]
          └─ calls TenantRepository.ReadTable [L125]
          └─ query Tenant [L125]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L125]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/internal/documents/find-documents  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.FindDocuments)  [L238–L294] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request FindDocumentsQuery -> FindDocumentsQueryHandler [L268]
        └─ [web] GET /core/v1/clients/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetAuditQuery)  [L256–L261] status=200
          └─ maps_to EntityAuditDto [L259]
          └─ uses_client ClientRepository [L259]
        └─ [web] GET /api/ui/documents/{documentId:guid}/versions/all  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetAll)  [L43–L56] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentVersionDto [L47]
            └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L424]
          └─ calls DocumentVersionRepository.ReadQuery [L47]
          └─ query DocumentVersion [L47]
            └─ reads_from DocumentVersions
        └─ [web] GET /api/stats/expiredtrials  (Workpapers.Next.API.Controllers.StatsController.ExpiredTrials)  [L30–L42] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetTrialExpiryQuery -> GetTrialExpiryQueryHandler [L39]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetTrialExpiryQueryHandler.Handle [L38–L84]
              └─ uses_service UnitOfWork
                └─ method Table [L54]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Search)  [L74–L88] status=200 [auth=Authentication.MasterPolicy]
          └─ sends_request FindTenantsQuery -> FindTenantsQueryHandler [L86]
        └─ [web] GET /api/sources/{type}/files  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAccountingFiles)  [L197–L203] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L200]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/binder-types/{id:guid}/record-templates  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetBinderRecordTemplates)  [L56–L92] status=200
          └─ maps_to BinderRecordTemplateDto [L84]
          └─ calls BinderTypeRecordTemplateSetRepository.ReadQuery [L59]
          └─ query BinderTypeRecordTemplateSet [L59]
            └─ reads_from BinderTypeRecordTemplateSets
        └─ [web] GET /api/internal/workpapers/binders/{binderId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.GetIndexAccountBalanceInfo)  [L35–L39] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ sends_request GetIndexAccountBalanceInfoQuery -> GetIndexAccountBalanceInfoQueryHandler [L38]
        └─ [web] GET /api/ui/document-store-users/{userId:guid}/invite  (Dataverse.Api.Controllers.UI.Documents.DocumentStoreUserController.GetInvite)  [L35–L55] status=200 [auth=Authentication.UserPolicy]
          └─ calls DocumentStoreUserRepository.ReadQuery [L38]
          └─ query DocumentStoreUser [L38]
            └─ reads_from DocumentStoreUsers
          └─ sends_request CreateNewDocumentInviteCommand -> CreateNewDocumentInviteCommandHandler [L51]
            └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
              └─ uses_service DocumentSecurityServiceFactory
                └─ method CreateInstance [L63]
                  └─ ... (no implementation details available)
              └─ uses_service IDocumentStoreService (AddScoped)
                └─ method GetReadOnlyDocumentStoresWithCredentials [L49]
                  └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
                    └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
                      └─ method GetReadOnlyDocumentStoresWithCredentials [L66]
                        └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.GetReadOnlyDocumentStoresWithCredentials
                    └─ uses_service TenantService
                      └─ method GetReadOnlyDocumentStoresWithCredentials [L57]
                        └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetReadOnlyDocumentStoresWithCredentials [L6-L27]
                          └─ uses_service TenantIdentificationService
                            └─ method GetCurrentTenant [L20]
                              └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
                    └─ maps_to SecureDocumentStoreDto [L66]
                      └─ automapper.registration DataverseMappingProfile (DocumentStore->SecureDocumentStoreDto) [L438]
                    └─ uses_cache IDistributedCache.SetRecordAsync [write] [L71]
                    └─ uses_cache IDistributedCache.GetRecordAsync [read] [L62]
              └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L49]
          └─ sends_request CreateNewDocumentInviteCommand -> CreateNewDocumentInviteCommandHandler [L48]
        └─ [web] GET /core/v1/offices/audits  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditsQuery)  [L98–L104] status=200
          └─ maps_to EntityAuditDto [L101]
          └─ calls OfficeRepository.ReadQuery [L101]
          └─ query Office [L101]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L101]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/reports/footer-templates/  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.GetAll)  [L44–L50] status=200 [auth=user]
          └─ maps_to FooterTemplateLightDto [L47]
            └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateLightDto) [L628]
          └─ calls FooterTemplateRepository.ReadQuery [L47]
          └─ query FooterTemplate [L47]
            └─ reads_from ReportFooterTemplates
        └─ [web] GET /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Search)  [L109–L114] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request FindBindersQuery -> FindBindersQueryHandler [L113]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindBindersQueryHandler.Handle [L48–L167]
              └─ calls ClientRepository.ReadQuery [L81]
              └─ uses_client ClientRepository [L81]
              └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
                └─ method ReadQuery [L104]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L90]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L89]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L89]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
        └─ [web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service AtoService
            └─ method GetActivityStatementSummary [L49]
              └─ implementation GovLink.Application.Services.AtoService.GetActivityStatementSummary [L12-L145]
        └─ [web] GET /api/subscriptions/byfirm/{id:Guid}  (Workpapers.Next.API.Controllers.SubscriptionsController.GetSubscriptions)  [L50–L59] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to SubscriptionDto [L53]
          └─ uses_service UnitOfWork
            └─ method Table [L53]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/internal/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetTaxAgentInfo)  [L97–L102] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TaxAgentInfoDto [L100]
            └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L229]
          └─ uses_client ClientRepository [L100]
        └─ [web] GET /api/internal/users  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAll)  [L83–L90] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserLightDto [L86]
            └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
          └─ calls UserRepository.ReadQuery [L86]
          └─ query User [L86]
          └─ uses_service UserRepository
            └─ method ReadQuery [L86]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/offices/{id:Guid}  (Workpapers.Next.API.Controllers.OfficeController.GetOffice)  [L73–L79] status=200
          └─ maps_to OfficeDto [L76]
          └─ uses_service UnitOfWork
            └─ method Table [L76]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /ledger/v1/standard-charts/{standardChartId:int}/accounts  (Cirrus.Api.External.Controllers.v1.Ledger.StandardChartsController.GetAccountsHierarchy)  [L60–L81] status=200
          └─ uses_service ICirrusProxyService (AddScoped)
            └─ method Get [L80]
              └─ implementation Cirrus.Api.External.Features.CirrusProxy.CirrusProxyService.Get [L10-L73]
                └─ uses_client CirrusClient [L26]
                └─ uses_service CirrusClient
                  └─ method Get [L26]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/internal/documents/{id:Guid}/download  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.DownloadDocument)  [L302–L310] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetDocumentDownloadLink -> GetDocumentDownloadLinkHandler [L307]
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L306]
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries  (DataGet.Api.Controllers.Integrations.IManageController.GetLibraries)  [L179–L184] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service IIManageService (AddScoped)
            └─ method GetLibraries [L183]
              └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetLibraries [L12-L223]
                └─ uses_client IManageApiClient [L33]
                └─ uses_service IManageApiClient
                  └─ method GetApiInformation [L33]
                    └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                └─ uses_service RequestProcessor
                  └─ method GetAuthorisationUrl [L28]
                    └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                    └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                    └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                    └─ +1 additional_requests elided
        └─ [web] GET /api/super/sync-monitor/summary  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetSummary)  [L93–L117] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls SyncConfigurationRepository.ReadQuery [L98]
          └─ calls DataverseEntityFailureLogRepository.ReadQuery [L96]
          └─ query SyncConfiguration [L98]
            └─ reads_from SyncConfigurations
          └─ query DataverseEntityFailureLog [L96]
            └─ reads_from DataverseEntityFailureLogs
        └─ [web] GET /api/productsets/{id:int}  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSet)  [L64–L74] status=200
          └─ maps_to ProductSetDto [L67]
          └─ uses_service UnitOfWork
            └─ method Table [L67]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/internal/account-mapping/mapping-codes/{code}  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodeByCode)  [L44–L48] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetMappingCodeQuery -> GetMappingCodeQueryHandler [L47]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodeQueryHandler.Handle [L38–L75]
              └─ maps_to MappingCodeDto [L68]
              └─ uses_service IControlledRepository<ExcludedMappingCode> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExcludedMappingCodeRepository.ReadQuery
              └─ uses_service UserService
                └─ method IsMaster [L59]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster (see previous expansion)
              └─ uses_service IControlledRepository<MappingCode> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.MappingCodeRepository.ReadQuery
        └─ [web] GET /api/ui/clients/{id:guid}/has-dependencies  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForExternalDependencies)  [L139–L145] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request CheckClientExternalDependenciesQuery -> CheckClientExternalDependenciesQueryHandler [L142]
            └─ handled_by Dataverse.ApplicationService.Queries.Clients.CheckClientExternalDependenciesQueryHandler.Handle [L35–L158]
              └─ uses_client CirrusClient [L115]
              └─ uses_client WorkpapersClient [L100]
              └─ uses_service TenantLicenseService
                └─ method GetFirmSubscriptions [L153]
                  └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
                    └─ uses_service TenantService
                      └─ method GetFirmSubscriptions [L74]
                        └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetFirmSubscriptions [L6-L27]
                          └─ uses_service TenantIdentificationService
                            └─ method GetCurrentTenant [L20]
                              └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
                    └─ maps_to SubscriptionDto [L76]
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L130]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service CirrusClient
                └─ method Get [L115]
                  └─ ... (no implementation details available)
              └─ uses_service WorkpapersClient
                └─ method Get [L100]
                  └─ ... (no implementation details available)
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L62]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L155]
              └─ uses_cache IDistributedCache.GetRecordAsync [read] [L146]
        └─ [web] GET /api/sources/export/{id:guid}/journal-for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetJournal)  [L61–L66] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetExportSourceJournalQuery -> GetExportSourceJournalQueryHandler [L64]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceJournalQueryHandler.Handle [L38–L265]
              └─ calls SourceAccountRepository.ReadQuery [L139]
              └─ maps_to SourceAccountUltraLightDto [L139]
                └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L106]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L90]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service ISwingingAccountService (AddScoped)
                └─ method GetSwingingAccounts [L80]
                  └─ implementation Workpapers.Next.Services.SwingingAccounts.SwingingAccountService.GetSwingingAccounts [L14-L91]
              └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
                └─ method ReadQuery [L69]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
        └─ [web] GET /api/clients/{id}  (Cirrus.Api.Controllers.Firm.ClientsController.Get)  [L93–L111] status=200 [auth=user]
          └─ maps_to ClientDto [L108]
            └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
          └─ calls ClientRepository.ReadQuery [L98]
          └─ query Client [L98]
        └─ [web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/internal/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetById)  [L62–L67] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SyncConfigurationInsecureDto [L65]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
          └─ calls SyncConfigurationRepository.ReadQuery [L65]
          └─ query SyncConfiguration [L65]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicies)  [L78–L92] status=200
          └─ maps_to OrderedPolicy [L91]
            └─ converts_to OrderedPolicy [L17]
          └─ uses_service UserService
            └─ method IsSuperUser [L82]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L88]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
              └─ uses_service UnitOfWork
                └─ method Table [L24]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/workpaper-records/linking-records  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetLinkingRecords)  [L123–L144] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorkpaperRecordDto [L133]
            └─ automapper.registration ExternalApiMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L169]
            └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L562]
          └─ calls WorkpaperRecordRepository.ReadQuery [L128]
          └─ query WorkpaperRecord [L128]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L126]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control-statements/{statementId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscStatement)  [L344–L352] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscStatementQuery -> GetCompanyPscStatementQueryHandler [L349]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscStatementQueryHandler.Handle [L19–L29]
        └─ [web] GET /api/companies-house-gateway/status-acknowledgement  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetStatusAcknowledgementAsync)  [L80–L84] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetStatusAcknowledgementQuery -> GetStatusAcknowledgementQueryHandler [L83]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetStatusAcknowledgementQueryHandler.Handle [L25–L53]
              └─ maps_to StatusAckDto [L51]
              └─ uses_client CompaniesHouseInputGatewayClient [L47]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L47]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L46]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/tradingAccounts/{id}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.Get)  [L41–L47] status=200 [auth=user]
          └─ maps_to TradingAccountDto [L44]
            └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountDto) [L227]
          └─ calls TradingAccountRepository.ReadQuery [L44]
          └─ query TradingAccount [L44]
            └─ reads_from TradingAccounts
        └─ [web] GET /api/super/users/{id:guid}  (Dataverse.Api.Controllers.Super.Core.UsersController.GetByTenant)  [L91–L96] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserDto [L95]
            └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
            └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
          └─ calls UserRepository.ReadQuery [L95]
          └─ query User [L95]
          └─ uses_service UserRepository
            └─ method ReadQuery [L95]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /core/v1/teams/  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.MinimalQuery)  [L68–L73] status=200
          └─ calls TeamRepository.ReadQuery [L71]
          └─ query Team [L71]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L71]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/companies-house-gateway/e-reminder  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetEReminderAsync)  [L100–L104] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetEReminderQuery -> GetEReminderQueryHandler [L103]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetEReminderQueryHandler.Handle [L33–L61]
              └─ maps_to ERemindersResponseDto [L59]
              └─ uses_client CompaniesHouseInputGatewayClient [L55]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L55]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L54]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/ui/workflow/job-filter-sets/can-i-save-levels  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.CanISaveLevels)  [L85–L106] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service UserService
            └─ method GetUser [L88]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUser [L15-L185]
                └─ calls UserRepository.ReadQuery [L133]
                └─ uses_service RequestInfoService
                  └─ method GetIdentityId [L160]
                    └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
                └─ uses_service User
                  └─ method GetUserId [L43]
                    └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                    └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
                └─ uses_service Guid?
                  └─ method GetUserId [L40]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.StartersController.Download)  [L131–L157] status=200
          └─ uses_client KeenClient [L148]
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L155]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L140]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L134]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L155]
          └─ sends_request GetProductQuery -> GetProductQueryHandler [L140]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control-statements  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscStatementsList)  [L386–L396] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscStatementsListQuery -> GetCompanyPscStatementsListQueryHandler [L393]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscStatementsListQueryHandler.Handle [L26–L38]
        └─ [web] GET /api/super/cirrus/databases  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetAllDatabases)  [L122–L126] status=200 [auth=Authentication.MasterPolicy]
          └─ uses_client CirrusClient [L125]
            └─ calls GetAll (GET /api/central/databases) [L125]
              └─ remote_endpoint_lookup route=/api/central/databases verb=GET
                └─ [web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
                  └─ maps_to DatabaseLightDto [L31]
                  └─ calls CentralRepository.ReadTable [L31]
                  └─ uses_service CentralRepository
                    └─ method ReadTable [L31]
                      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
        └─ [web] GET /workflow/v1/task-templates/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.Get)  [L49–L55] status=200
          └─ maps_to TaskTemplateDto [L52]
            └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
          └─ calls TaskTemplateRepository.ReadQuery [L52]
          └─ query TaskTemplate [L52]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/field-groups  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFieldGroups)  [L134–L149] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTemplateFieldGroupDto [L141]
            └─ automapper.registration DataverseMappingProfile (DocumentTemplateFieldGroup->DocumentTemplateFieldGroupDto) [L436]
          └─ calls DocumentTemplateFieldGroupRepository.ReadQuery [L141]
          └─ query DocumentTemplateFieldGroup [L141]
            └─ reads_from DocumentTemplateFieldGroups
        └─ [web] GET /api/sources/types  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetSourceTypes)  [L108–L112] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetSourceTypesQuery -> GetSourceTypesQueryHandler [L111]
        └─ [web] GET /workpapers/v1/worksheets/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsDetailedQuery)  [L77–L83] status=200
          └─ calls WorksheetRepository.ReadQuery [L80]
          └─ query Worksheet [L80]
            └─ reads_from Worksheets
        └─ [web] GET /api/internal/notification-subscriptions/record-templates  (Workpapers.Next.API.Controllers.Internal.NotificationSubscriptionController.GetUpdatedRecordTemplatesQuery)  [L26–L31] status=200 [auth=AuthorizationPolicies.M2M]
          └─ sends_request GetUpdatedRecordTemplateQuery -> GetUpdatedRecordTemplateQueryHandler [L29]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.NotificationSubscriptions.GetUpdatedRecordTemplateQueryHandler.Handle [L22–L62]
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L33]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
        └─ [web] GET /api/internal/storage/refresh-template-storage  (Dataverse.Api.Controllers.Internal.StorageController.RefreshTemplateStorage)  [L256–L262] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service ITemplateService (AddSingleton)
            └─ method GetTemplates [L259]
              └─ implementation Dataverse.Templates.TemplateService.GetTemplates [L17-L359]
                └─ uses_service TemplateConfiguration
                  └─ method GetSiteDriveId [L333]
                    └─ ... (no implementation details available)
                └─ uses_service SharePointTemplateManager
                  └─ method GetDownloadUrl [L82]
                    └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/templates/{id}/policies  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPolicies)  [L104–L108] status=200 [auth=user]
          └─ sends_request GetDefaultPoliciesForExistingReportQuery -> GetDefaultPoliciesForExistingReportQueryHandler [L107]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultPoliciesForExistingReportQueryHandler.Handle [L44–L105]
              └─ calls ReportContentRepository.LoadReadProperties [L72]
              └─ maps_to PolicySelectorModel [L74]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L87]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
                └─ method ReadQuery [L64]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
        └─ [web] GET /api/binder-sections/for-binder/{binderId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderSectionsController.GetForBinder)  [L53–L66] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to BinderSectionDto [L58]
            └─ automapper.registration WorkpapersMappingProfile (BinderSection->BinderSectionDto) [L465]
            └─ automapper.registration ExternalApiMappingProfile (BinderSection->BinderSectionDto) [L189]
          └─ calls BinderSectionRepository.ReadQuery [L58]
          └─ query BinderSection [L58]
            └─ reads_from BinderSections
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L56]
        └─ [web] GET /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L36] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetBenchmarkCodeSetLightQuery -> GetBenchmarkCodeSetQueryHandler [L35]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetQueryHandler.Handle [L37–L64]
              └─ maps_to BenchmarkCodeSetDto [L59]
                └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetDto) [L248]
              └─ maps_to BenchmarkCodeSetLightDto [L51]
                └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L247]
              └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
                └─ method ReadQuery [L51]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.ReadQuery
        └─ [web] GET /api/workpaper-record-templates/archive-maps  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetArchivedWorkpaperRecordTemplateMappings)  [L193–L201] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to ArchivedWorkpaperRecordTemplateMappingDto [L198]
            └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingDto) [L365]
          └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L198]
          └─ query ArchivedWorkpaperRecordTemplateMapping [L198]
            └─ reads_from ArchivedWorkpaperRecordTemplateMappings
        └─ [web] GET /api/sources/{type}/creditors  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetCreditors)  [L283–L310] status=200 [auth=AuthorizationPolicies.User]
          └─ calls WorkpaperRecordRepository.ReadQuery [L301]
          └─ query WorkpaperRecord [L301]
            └─ reads_from WorkpaperRecords
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L307]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L288]
        └─ [web] GET /api/companies-house/company/{companyNumber}/charges  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharges)  [L161–L169] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyChargesQuery -> GetCompanyChargesQueryHandler [L166]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargesQueryHandler.Handle [L21–L33]
        └─ [web] GET /api/accounting/tax-forms/  (Cirrus.Api.Controllers.Accounting.Tax.TaxFormsController.GetForms)  [L26–L30] status=200 [auth=user]
        └─ [web] GET /api/sources/{type}/wages  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetWages)  [L260–L281] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L278]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/commands  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetRecordCommands)  [L140–L154] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to CommandDto [L145]
          └─ calls WorksheetRepository.ReadQuery [L145]
          └─ query Worksheet [L145]
            └─ reads_from Worksheets
          └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L143]
        └─ [web] GET /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSets)  [L43–L48] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetBenchmarkCodeSetsQuery -> GetBenchmarkCodeSetsQueryHandler [L47]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetsQueryHandler.Handle [L24–L43]
              └─ maps_to BenchmarkCodeSetLightDto [L37]
                └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L247]
              └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
                └─ method ReadQuery [L37]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.ReadQuery
        └─ [web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystem)  [L33–L37] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/internal/account-mapping/mapping-codes/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodeById)  [L32–L36] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetMappingCodeQuery -> GetMappingCodeQueryHandler [L35]
        └─ [web] GET /api/connections/class/funds/{fundCode}/ledger/{accountId}/{date:datetime}  (Workpapers.Next.API.Controllers.Connections.ClassController.GetLedger)  [L61–L67] status=200
        └─ [web] GET /api/super/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetErrors)  [L85–L116] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls SyncConfigurationRepository.ReadQuery [L95]
          └─ query SyncConfiguration [L95]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/dataverse/firms/{dataverseId}/settings  (Workpapers.Next.API.Controllers.DataverseController.GetFirmSettings)  [L216–L228] status=200 [auth=AuthorizationPolicies.M2M]
          └─ maps_to WorkpaperSettingsDto [L220]
          └─ uses_service FirmFeatureFlagService
            └─ method GetAvailableFeaturesForFirm [L225]
              └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L220]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/connections/{type}/accountingFiles  (Cirrus.Api.Controllers.Accounting.ConnectionsController.GetAccountingFiles)  [L54–L58] status=200 [auth=user]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetApiMethods [L57]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/accounting-file/{fileId}/legacy-trialbalance  (DataGet.Api.Controllers.Connections.AccountingFileController.LegacyGetTrialBalance)  [L157–L165] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /workpapers/v1/workpaper-records/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsMinimalQuery)  [L58–L64] status=200
          └─ calls WorkpaperRecordRepository.ReadQuery [L61]
          └─ query WorkpaperRecord [L61]
            └─ reads_from WorkpaperRecords
        └─ [web] GET /api/internal/data-cloud  (Dataverse.Api.Controllers.Internal.DataCloudController.HandleRequestAsync)  [L92–L104] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service UnitOfWork
            └─ method Table [L98]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPersonBeneficialOwner)  [L324–L332] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscLegalPersonBeneficialOwnerQuery -> GetCompanyPscLegalPersonBeneficialOwnerQueryHandler [L329]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonBeneficialOwnerQueryHandler.Handle [L19–L31]
        └─ [web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}/mapping-codes/{mappingCodeId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCode)  [L94–L99] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodeQuery -> GetExternalReportingSystemMappingCodeQueryHandler [L98]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodeQueryHandler.Handle [L30–L50]
              └─ maps_to ExternalReportingSystemMappingCodeDto [L46]
                └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
              └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
                └─ method ReadQuery [L46]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
        └─ [web] GET /api/users/all  (Workpapers.Next.API.Controllers.UsersController.GetAll)  [L56–L70] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetPagedUsersQuery [L69]
        └─ [web] GET /api/fyi/users  (DataGet.Api.Controllers.Integrations.FyiController.GetUsers)  [L217–L226] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/ato/individual-income-tax-returns  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetIndividualIncomeTaxReturns)  [L74–L80] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetIndividualIncomeTaxReturnQuery -> GetIndividualIncomeTaxReturnQueryHandler [L77]
          └─ returns IndividualIncomeTaxReturnDto [L77]
        └─ [web] GET /api/matter-text-templates/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetForBinder)  [L80–L109] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IMatterTextParsingService (AddScoped)
            └─ method ParseMatterText [L103]
              └─ implementation Workpapers.Next.ApplicationService.Features.MatterTextParsing.MatterTextParsingService.ParseMatterText [L18-L194]
                └─ uses_client ClientRepository [L124]
                └─ uses_client Client [L122]
                └─ uses_service Binder
                  └─ method ParseMatterText [L57]
                    └─ implementation Workpapers.Next.DomainModel.Model.Workpapers.Binder.ParseMatterText [L52-L929]
          └─ sends_request GetMatterTextTemplatesWithWorkpaperRecordTemplateQuery [L97]
        └─ [web] GET /api/sections/{id:Guid}  (Workpapers.Next.API.Controllers.SectionsController.Get)  [L48–L54] status=200
          └─ maps_to SectionLightDto [L51]
          └─ uses_service UnitOfWork
            └─ method Table [L51]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/filing-history/{transactionId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistoryItem)  [L193–L201] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyFilingHistoryItemQuery -> GetCompanyFilingHistoryItemQueryHandler [L198]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryItemQueryHandler.Handle [L19–L30]
        └─ [web] GET /ledger/v1/datasets/{datasetId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.Get)  [L79–L84] status=200
          └─ maps_to DatasetDto [L82]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
            └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
          └─ calls DatasetRepository.ReadQuery [L82]
          └─ query Dataset [L82]
        └─ [web] GET /workflow/v1/task-templates/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.MinimalQuery)  [L67–L75] status=200
          └─ calls TaskTemplateRepository.ReadQuery [L71]
          └─ query TaskTemplate [L71]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Get)  [L50–L54] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetSyncConfigurationQuery -> GetSyncConfigurationQueryHandler [L53]
        └─ [web] GET /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.GetAll)  [L53–L63] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to DeliverableTypeDto [L56]
            └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
            └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
          └─ calls DeliverableTypeRepository.ReadQuery [L56]
          └─ query DeliverableType [L56]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/job-types/{id:Guid}  (Workpapers.Next.API.Controllers.JobTypesController.Get)  [L43–L51] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to JobTypeLightDto [L46]
            └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeLightDto) [L868]
          └─ calls JobTypeRepository.ReadQuery [L46]
          └─ query JobType [L46]
            └─ reads_from JobTypes
          └─ uses_service JobTypeRepository
            └─ method ReadQuery [L46]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.JobTypeRepository.ReadQuery [L8-L38]
        └─ [web] GET /api/karbon  (DataGet.Api.Controllers.Integrations.KarbonController.SetConnectionContext)  [L77–L80] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/internal/binder-data/tax  (Dataverse.Api.Controllers.Internal.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L31] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetDocumentDataQuery -> GetDocumentDataQueryHandler [L30]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L63]
              └─ uses_service DocumentServiceFactory
                └─ method GetDocumentWithService [L54]
                  └─ implementation DocumentServiceFactory.GetDocumentWithService (see previous expansion)
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method WriteQuery [L43]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
        └─ [web] GET /api/connection/  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnections)  [L34–L38] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetConnectionsQuery -> GetConnectionsQueryHandler [L37]
            └─ handled_by DataGet.Connections.App.Queries.GetConnectionsQueryHandler.Handle [L14–L78]
              └─ calls FileTokenRepository.ReadQuery [L40]
              └─ calls UserTokenRepository.ReadQuery [L32]
              └─ uses_service RequestContextProvider
                └─ method CurrentIdentification [L29]
                  └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.CurrentIdentification
        └─ [web] GET /api/accounting/ledger/accounttypes/workpaper-sections  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.GetWithWorkpaperSections)  [L63–L69] status=200 [auth=user]
          └─ maps_to AccountTypeWithWorkpaperSectionDto [L66]
            └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeWithWorkpaperSectionDto) [L252]
          └─ calls AccountTypeRepository.ReadQuery [L66]
          └─ query AccountType [L66]
            └─ reads_from AccountTypes
        └─ [web] GET /workflow/v1/job-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.Get)  [L52–L57] status=200
          └─ maps_to JobTypeDto [L55]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
            └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
          └─ calls JobTypeRepository.ReadQuery [L55]
          └─ query JobType [L55]
            └─ reads_from JobTypes
        └─ [web] GET /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetById)  [L67–L75] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentStatusDto [L70]
            └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
          └─ calls DocumentStatusRepository.ReadQuery [L70]
          └─ query DocumentStatus [L70]
            └─ reads_from DVS_DocumentStatuses
        └─ [web] GET /api/accounting/reports/masters  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetReportMasterParameters)  [L178–L189] status=200 [auth=user]
          └─ calls ReportPageTypeRepository.WriteQuery [L185]
          └─ write ReportPageType [L185]
            └─ reads_from ReportPageTypes
        └─ [web] GET /api/connection/{apiType}/authorisation-url  (DataGet.Api.Controllers.Connections.ConnectionController.GetAuthorisationUrl)  [L46–L54] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetAuthorizationUrlQuery -> GetAuthorizationUrlQueryHandler [L50]
            └─ handled_by DataGet.Connections.App.Queries.GetAuthorizationUrlQueryHandler.Handle [L23–L60]
              └─ uses_service RequestProcessor
                └─ method Process [L53]
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.Process
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.Process
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.Process
                  └─ +1 additional_requests elided
              └─ uses_service ExternalApiServiceFactory
                └─ method GetExternalApiFromApiType [L51]
                  └─ ... (no implementation details available)
              └─ uses_service ConnectionContextProvider
                └─ method ConnectionContext [L39]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/import-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Get)  [L51–L64] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to JournalLightDto [L60]
          └─ maps_to ImportRunDto [L54]
            └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunDto) [L817]
          └─ calls ImportRunRepository.ReadQuery [L54]
          └─ query ImportRun [L54]
            └─ reads_from ImportRuns
          └─ uses_service ImportRunRepository
            └─ method ReadQuery [L54]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.ImportRunRepository.ReadQuery (see previous expansion)
          └─ sends_request GetImportRunJournalsQuery -> GetImportRunJournalsQueryHandler [L59]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetImportRunJournalsQueryHandler.Handle [L28–L83]
              └─ maps_to JournalCreateModel [L59]
                └─ converts_to JournalLightDto [L491]
                └─ converts_to JournalLightDto [L861]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L57]
        └─ [web] GET /api/accounting/assets/reports/tax-return/sbe  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetTaxReturnReportSbe)  [L182–L193] status=200 [auth=user]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L185]
        └─ [web] GET /api/connections/class/funds/{fundCode}/property/{accountName}/{date:datetime}  (Workpapers.Next.API.Controllers.Connections.ClassController.GetProperty)  [L53–L59] status=200
        └─ [web] GET /api/ui/imanage/workspaces/{workspaceId}/folders  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaceFolders)  [L200–L215] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L203]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L203]
          └─ query IntegrationSettings [L203]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetWorkspaceFoldersAsync [L209]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetWorkspaceFoldersAsync [L19-L225]
        └─ [web] GET /api/super/tenants/{tenantId}/sso/open-id/configs/{configId}  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.GetOpenIdConfigById)  [L38–L43] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service IOpenIdService (AddScoped)
            └─ method GetOpenIdConfigByIdAsync [L41]
              └─ implementation Dataverse.Services.Features.SSO.OpenIdService.GetOpenIdConfigByIdAsync [L9-L53]
                └─ uses_service IdentityService
                  └─ method AddOpenIdConfigAsync [L23]
                    └─ implementation Dataverse.Services.Features.Identity.IdentityService.AddOpenIdConfigAsync [L14-L67]
                      └─ uses_service EnvironmentConfig
                        └─ method GetStandardQuery [L53]
                          └─ ... (no implementation details available)
        └─ [web] GET /api/ui/trials/valid-email  (Dataverse.Api.Controllers.UI.Trial.TrialsController.ValidateEmail)  [L32–L37] status=200 [AllowAnonymous]
          └─ sends_request ValidateTrialEmailQuery -> ValidateTrialEmailQueryHandler [L36]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Trials.ValidateTrialEmailQueryHandler.Handle [L37–L134]
              └─ calls TenantRepository (methods: ReadTable,WriteTable) [L127]
              └─ uses_service List<string>
                └─ method Any [L118]
                  └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.Any [L60-L77]
                    └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                    └─ uses_service IRequestProcessor (InstancePerDependency)
                      └─ method ProcessAsync [L70]
                        └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
                    └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                      └─ method ReadQuery [L66]
                        └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                    └─ query PublishedReportBatch [L66]
                    └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
                    └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
        └─ [web] GET /api/reportsettings/reporttemplates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Download)  [L129–L142] status=200
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L140]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L132]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L140]
        └─ [web] GET /api/super/smart-workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.GetSubscription)  [L63–L78] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L68]
          └─ uses_client WorkpapersClient [L75]
            └─ calls CreateFirm (POST /api/dataverse/firms/) [L110]
              └─ remote_endpoint_expansion_suppressed (see previous expansion)
            └─ calls Search [L60]
              └─ remote_endpoint_expansion_suppressed (see previous expansion)
          └─ calls TenantRepository.ReadTable [L68]
          └─ query Tenant [L68]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L68]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/connections/qbo/files  (Workpapers.Next.API.Controllers.Connections.QBOController.GetAccountingFiles)  [L27–L33] status=200
        └─ [web] GET /api/internal/account-mapping/external-reporting-systems/mapping-codes  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCodesByCode)  [L63–L68] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodesByCodeQuery -> GetExternalReportingSystemMappingCodesByCodeQueryHandler [L66]
        └─ [web] GET /api/clients/  (Cirrus.Api.Controllers.Firm.ClientsController.Search)  [L57–L75] status=200 [auth=user]
        └─ [web] GET /api/ui/contacts/{id}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Get)  [L83–L93] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ContactDto [L88]
            └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
          └─ calls ContactRepository.ReadQuery [L88]
          └─ query Contact [L88]
            └─ reads_from DVS_Clients
          └─ sends_request CanIAccessContactQuery -> CanIAccessContactQueryHandler [L86]
            └─ handled_by Dataverse.ApplicationService.Queries.Contacts.CanIAccessContactQueryHandler.Handle [L36–L90]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettingsAsync [L76]
                  └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
              └─ uses_service IControlledRepository<Contact> (Scoped (inferred))
                └─ method ReadQuery [L74]
                  └─ implementation Dataverse.Data.Repository.Clients.ContactRepository.ReadQuery
              └─ uses_service TenantService
                └─ method GetCurrentTenantAsync [L69]
                  └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L68]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L65]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
              └─ uses_cache IDistributedCache.SetRecordAsync [write] [L86]
              └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L72]
              └─ uses_cache IDistributedCache.CreateAccessKey [write] [L70]
        └─ [web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetChildStandardAccounts)  [L218–L235] status=200
          └─ calls SourceAccountRepository.ReadQuery [L228]
          └─ query SourceAccount [L228]
            └─ reads_from SourceAccounts
          └─ uses_service SourceAccountRepository
            └─ method ReadQuery [L228]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery (see previous expansion)
          └─ sends_request GetChildStandardAccountsAsLightDtoQuery [L222]
        └─ [web] GET /api/health  (Cirrus.Api.Controllers.HealthController.Alive)  [L10–L14] status=200 [AllowAnonymous]
        └─ [web] GET /api/accounting/tax/taxonomy/report  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetReport)  [L47–L51] status=200 [auth=user]
          └─ sends_request GetTaxonomyReport -> GetTaxonomyReportQueryHandler [L50]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Tax.GetTaxonomyReportQueryHandler.Handle [L43–L148]
              └─ maps_to AccountTypeLightWithTaxonomyDto [L112]
              └─ maps_to TaxonomyDto [L106]
              └─ maps_to AccountLightListDto [L135]
              └─ uses_service GetAccountsQuery
                └─ method Execute [L79]
                  └─ resolves_request Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute
                  └─ resolves_request DataGet.Connections.App.Queries.GetAccountsQuery.Execute
                  └─ resolves_request DataGet.Connections.External.Bgl360.Api.Queries.GetAccountsQuery.Execute
                  └─ +4 additional_requests elided
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method Execute [L78]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L71]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L66]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
        └─ [web] GET /api/accounting-file/{fileId}/movement-journals  (DataGet.Api.Controllers.Connections.AccountingFileController.GetMovements)  [L54–L63] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetMovementJournalsQuery -> GetMovementJournalsQueryHandler [L59]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetMovementJournalsQueryHandler.Handle [L49–L204]
              └─ uses_client DataGetClient [L81]
                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                  └─ remote_endpoint_expansion_suppressed (see previous expansion)
              └─ uses_service IControlledRepository<SourceDivision> (Scoped (inferred))
                └─ method ReadQuery [L187]
                  └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceDivisionRepository.ReadQuery
              └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
                └─ method Add [L116]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.Add
              └─ uses_service DataGetClient
                └─ method GetAccountsAsync [L81]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync (see previous expansion)
              └─ uses_service ApiService (SingleInstance)
                └─ method GetFeatures [L77]
                  └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L76]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
        └─ [web] GET /api/users/{id:Guid}  (Workpapers.Next.API.Controllers.UsersController.Get)  [L108–L117] status=200
          └─ maps_to UserDto [L111]
            └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
          └─ calls UserRepository.ReadQuery [L111]
          └─ query User [L111]
          └─ uses_service UserRepository
            └─ method ReadQuery [L111]
              └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting-file/{fileId}/creditors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors)  [L109–L117] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCreditorsQuery -> GetCreditorsQueryHandler [L113]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetCreditorsQueryHandler.Handle [L25–L57]
              └─ uses_client DataGetClient [L53]
                └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetCreditorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                  └─ target_service DataGet.Api
                    └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors
              └─ uses_service DataGetClient
                └─ method GetCreditorsAsync [L53]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetCreditorsAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L46]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L40]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
          └─ returns Creditors [L113]
        └─ [web] GET /api/accounting/assets/reports/full-summary/fixed-assets-workpaper-report  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetFixedAssetsWorkpaperReport)  [L70–L75] status=200 [auth=user]
        └─ [web] GET /api/job-types/search  (Workpapers.Next.API.Controllers.JobTypesController.Search)  [L60–L66] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request FindJobTypesQuery -> FindJobTypesQueryHandler [L63]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindJobTypesQueryHandler.Handle [L32–L51]
              └─ calls JobTypeRepository.ReadQuery [L45]
        └─ [web] GET /api/super/sync-monitor/count  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetCount)  [L84–L91] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls DataverseEntityFailureLogRepository.ReadQuery [L90]
          └─ calls SyncConfigurationRepository.ReadQuery [L87]
          └─ query DataverseEntityFailureLog [L90]
            └─ reads_from DataverseEntityFailureLogs
          └─ query SyncConfiguration [L87]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/ui/account-mapping/mapping-codes/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.GetMappingCodeById)  [L32–L36] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/ui/fyi  (Dataverse.Api.Controllers.UI.FyiController.GetDownloadUrl)  [L293–L300] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service StorageService
            └─ method CopyFileFromUri [L297]
              └─ implementation Dataverse.Services.Features.Storage.StorageService.CopyFileFromUri (see previous expansion)
          └─ uses_storage StorageService.CopyFileFromUri [L297]
        └─ [web] GET /api/accounting/reports/templates/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetAll)  [L160–L169] status=200 [auth=user]
          └─ maps_to ReportTemplateLightDto [L164]
            └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateLightDto) [L546]
          └─ calls ReportTemplateRepository.ReadQuery [L164]
          └─ query ReportTemplate [L164]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L163]
        └─ [web] GET /api/sources/export/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetForClient)  [L44–L48] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetExportSourcesForClientQuery -> GetExportSourcesForClientQueryHandler [L47]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourcesForClientQueryHandler.Handle [L29–L47]
              └─ maps_to ExportSourceLightDto [L42]
                └─ automapper.registration WorkpapersMappingProfile (ExportSource->ExportSourceLightDto) [L601]
              └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
                └─ method ReadQuery [L42]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
        └─ [web] GET /api/internal/support-users/expired-users  (Dataverse.Api.Controllers.Internal.SupportUsersController.GetExpiredSupportUsers)  [L46–L64] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls TenantRepository.ReadTable [L50]
          └─ query Tenant [L50]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L50]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/workpaper-record-templates/{id:Guid}/download-template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.DownloadTemplate)  [L84–L89] status=200
          └─ sends_request DownloadTemplateQuery -> DownloadTemplateQueryHandler [L88]
          └─ sends_request GetWorkpaperRecordTemplateToInsertQuery -> GetWorkpaperRecordTemplateToInsertQueryHandler [L87]
        └─ [web] GET /api/binder-export-defaults/  (Workpapers.Next.API.Controllers.Workpapers.BinderExportDefaultsController.Get)  [L33–L41] status=200
          └─ maps_to BinderExportDefaultsDto [L36]
            └─ automapper.registration WorkpapersMappingProfile (BinderExportDefaults->BinderExportDefaultsDto) [L910]
          └─ calls BinderExportDefaultsRepository.ReadQuery [L36]
          └─ query BinderExportDefaults [L36]
            └─ reads_from BinderExportDefaults
        └─ [web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Search)  [L60–L64] status=200
          └─ sends_request FindWorkpaperRecordTemplatesQuery -> GetWorkpaperRecordTemplatesPagedQueryHandler [L63]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetWorkpaperRecordTemplatesPagedQueryHandler.Handle [L62–L277]
              └─ maps_to SectionLightDto [L111]
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L194]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
                └─ method ReadQuery [L130]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
              └─ uses_service UnitOfWork
                └─ method Table [L97]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service UserService
                └─ method IsSuperUser [L97]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
        └─ [web] GET /api/fyi/cabinets  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinets)  [L45–L54] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCabinetsQuery -> GetCabinetsQueryHandler [L50]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetsQueryHandler.Handle [L19–L36]
              └─ uses_service FyiService
                └─ method GetCabinets [L30]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCabinets [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/public/sync-services/  (Dataverse.Api.Controllers.Public.SyncServicesController.GetAll)  [L37–L43] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to SyncServiceDto [L40]
            └─ automapper.registration DataverseMappingProfile (SyncService->SyncServiceDto) [L260]
          └─ calls SyncServiceRepository.ReadQuery [L40]
          └─ query SyncService [L40]
            └─ reads_from SyncServices
        └─ [web] GET /api/accounting/reports/saved-reports/download/{id}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.Export)  [L66–L80] status=200 [auth=user]
          └─ calls PublishedReportFileRepository.ReadQuery [L69]
          └─ query PublishedReportFile [L69]
            └─ reads_from PublishedReportFiles
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L73]
        └─ [web] GET /api/audit-trail/  (Cirrus.Api.Controllers.Firm.AuditHistoriesController.GetForAuditedEntity)  [L34–L44] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to AuditHistoryDto [L37]
            └─ automapper.registration CirrusMappingProfile (AuditHistory->AuditHistoryDto) [L187]
            └─ converts_to ExportAuditHistoryDto [L114]
          └─ calls AuditHistoryRepository.ReadQuery [L37]
          └─ query AuditHistory [L37]
            └─ reads_from AuditHistories
        └─ [web] GET /api/gov-link/client-accounts/test/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetailTest)  [L45–L52] status=200 [AllowAnonymous]
          └─ uses_service TestService
            └─ method GetClientAccountTransactions [L51]
              └─ ... (no implementation details available)
        └─ [web] GET /api/karbon/test-connection  (DataGet.Api.Controllers.Integrations.KarbonController.TestConnection)  [L53–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request TestConnectionQuery -> TestConnectionQueryHandler [L58]
            └─ handled_by Cirrus.Connections.DataGet.Queries.TestConnectionQueryHandler.Handle [L17–L31]
              └─ uses_client DataGetClient [L28]
                └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, method=TestConnectionAsync, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ sends_request TestConnectionCommand -> TestConnectionCommandHandler [L114]
              └─ uses_service DataGetClient
                └─ method TestConnectionAsync [L28]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.TestConnectionAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
        └─ [web] GET /api/sources/export/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Get)  [L34–L38] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetExportSourceQuery -> GetExportSourceQueryHandler [L37]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceQueryHandler.Handle [L26–L43]
              └─ maps_to ExportSourceDto [L39]
                └─ automapper.registration WorkpapersMappingProfile (ExportSource->ExportSourceDto) [L602]
              └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
                └─ method ReadQuery [L39]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
        └─ [web] GET /api/ui/fyi/groups  (Dataverse.Api.Controllers.UI.FyiController.GetGroups)  [L158–L164] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetGroupsAsync [L161]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetGroupsAsync [L19-L291]
        └─ [web] GET /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetAll)  [L37–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to KanbanColumnDto [L40]
            └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
            └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
          └─ calls KanbanColumnRepository.ReadQuery [L40]
          └─ query KanbanColumn [L40]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.Get)  [L51–L57] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to JobStatusDto [L54]
            └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
            └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
          └─ calls JobStatusRepository.ReadQuery [L54]
          └─ query JobStatus [L54]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/ui/workflow/jobs/series  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeries)  [L112–L136] status=200 [auth=Authentication.UserPolicy]
          └─ calls JobRepository.ReadQuery [L121]
          └─ query Job [L121]
            └─ reads_from Jobs
        └─ [web] GET /core/v1/offices/detailed  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.FullQuery)  [L86–L91] status=200
          └─ calls OfficeRepository.ReadQuery [L89]
          └─ query Office [L89]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L89]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/uk-establishments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyUkEstablishments)  [L254–L262] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyUkEstablishmentsQuery -> GetCompanyUkEstablishmentsQueryHandler [L259]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyUkEstablishmentsQueryHandler.Handle [L15–L24]
        └─ [web] GET /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Get)  [L35–L39] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetLoanMatrixQuery -> GetLoanMatrixQueryHandler [L38]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixQueryHandler.Handle [L31–L86]
              └─ calls ClientRepository.ReadQuery [L74]
              └─ maps_to LoanMatrixDto [L58]
                └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
              └─ uses_client ClientRepository [L74]
              └─ uses_service FirmSettingsService
                └─ method GetCurrentSettings [L76]
                  └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L76]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
                └─ method ReadQuery [L58]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
        └─ [web] GET /api/accounting/reports/styles/excel/download/master  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportExcelController.DownloadMaster)  [L41–L49] status=200 [auth=Authentication.AdminPolicy]
        └─ [web] GET /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Get)  [L43–L49] status=200 [auth=user]
          └─ maps_to OfficeDto [L46]
            └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
          └─ calls OfficeRepository.ReadQuery [L46]
          └─ query Office [L46]
            └─ reads_from Offices
        └─ [web] GET /api/accounting-file/{fileId}/legacy-rental  (DataGet.Api.Controllers.Connections.AccountingFileController.LegacyGetRentalQuery)  [L187–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/connection/{apiType}/accounting-files  (DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles)  [L100–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetAccountingFilesQuery -> GetAccountingFilesQueryHandler [L104]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountingFilesQueryHandler.Handle [L15–L38]
              └─ uses_client DataGetClient [L26]
                └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, method=GetAccountingFilesAsync, target=DataGet.Api) [L52]
                  └─ target_service DataGet.Api
                    └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles
              └─ uses_service DataGetClient
                └─ method GetAccountingFilesAsync [L26]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountingFilesAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
        └─ [web] GET /core/v1/tax-agents/  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.GetAll)  [L55–L79] status=200
          └─ maps_to TaxAgentDto [L70]
            └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
            └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
          └─ calls TaxAgentRepository.ReadQuery [L63]
          └─ query TaxAgent [L63]
            └─ reads_from TaxAgents
        └─ [web] GET /api/ui/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetErrors)  [L79–L110] status=200 [auth=Authentication.AdminPolicy]
          └─ calls SyncConfigurationRepository.ReadQuery [L89]
          └─ query SyncConfiguration [L89]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stage-definitions  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStageDefinitions)  [L52–L58] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetStageDefinitionsForAutomationRunQuery -> GetStageDefinitionsForAutomationRunQueryHandler [L57]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStageDefinitionsForAutomationRunQueryHandler.Handle [L30–L53]
              └─ maps_to StageDefinitionDto [L51]
              └─ uses_service BotService
                └─ method GetStageDefinitions [L51]
                  └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetStageDefinitions (see previous expansion)
              └─ uses_service IControlledRepository<AutomationRun> (Scoped (inferred))
                └─ method ReadQuery [L45]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.AutomationRunRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L55]
        └─ [web] GET /api/accounting/reports/templates/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPackage)  [L144–L148] status=200 [auth=user]
          └─ sends_request ReportTemplatePackageQuery -> ReportTemplatePackageQueryHandler [L147]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplatePackageQueryHandler.Handle [L50–L129]
              └─ maps_to ReportingSuiteUltraLightDto [L123]
                └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteUltraLightDto) [L766]
              └─ maps_to ReportPageLayoutLightDto [L119]
                └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
              └─ maps_to FooterTemplateDto [L116]
                └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
                └─ converts_to FooterContentDto [L49]
              └─ maps_to HeaderTemplateLightDto [L113]
                └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
              └─ maps_to TradingAccountLightDto [L109]
                └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountLightDto) [L228]
              └─ maps_to DivisionLightDto [L105]
                └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
              └─ maps_to DatasetLightDto [L101]
                └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
              └─ maps_to ReportWatermarkDto [L87]
                └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
              └─ maps_to ReportSettingsLightDto [L83]
                └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
              └─ uses_service IControlledRepository<ReportingSuite> (Scoped (inferred))
                └─ method ReadQuery [L123]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.ReportingSuiteRepository.ReadQuery
              └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
                └─ method ReadQuery [L119]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
              └─ uses_service IControlledRepository<FooterTemplate> (Scoped (inferred))
                └─ method ReadQuery [L116]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.FooterTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<HeaderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L113]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.HeaderTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<TradingAccount> (Scoped (inferred))
                └─ method ReadQuery [L109]
                  └─ implementation Cirrus.Data.Repository.Accounting.Setup.TradingAccountRepository.ReadQuery
              └─ uses_service IControlledRepository<Division> (Scoped (inferred))
                └─ method ReadQuery [L105]
                  └─ implementation Cirrus.Data.Repository.Accounting.Setup.DivisionRepository.ReadQuery
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L101]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IJurisdictionService (AddScoped)
                └─ method GetUserJurisdictions [L97]
                  └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetUserJurisdictions (see previous expansion)
              └─ uses_service IControlledRepository<ReportWatermark> (Scoped (inferred))
                └─ method ReadQuery [L87]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.WatermarkRepository.ReadQuery [L7-L42]
              └─ uses_service IControlledRepository<ReportSettings> (Scoped (inferred))
                └─ method ReadQuery [L83]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportSettingsRepository.ReadQuery
        └─ [web] GET /api/accounting/reports/notes/policies/variants  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.CanIEditPolicyVariant)  [L159–L170] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method IsInRole [L163]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L168]
        └─ [web] GET /api/officeUsers/{id}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Get)  [L41–L47] status=200 [auth=user]
          └─ maps_to OfficeUserDto [L44]
            └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserDto) [L130]
          └─ calls OfficeUserRepository.ReadQuery [L44]
          └─ query OfficeUser [L44]
            └─ reads_from OfficeUsers
        └─ [web] GET /api/connections/reportance/taxonomy  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetTaxonomy)  [L116–L122] status=200
        └─ [web] GET /api/teams/byfirm/{id:Guid}  (Workpapers.Next.API.Controllers.TeamController.GetTeamsForFirm)  [L45–L56] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to TeamDto [L51]
          └─ uses_service UnitOfWork
            └─ method Table [L51]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/licensing/{productExcelId:int}  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackageForProduct)  [L97–L130] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to FirmDto (var Firm) [L106]
            └─ converts_to FirmWithStatsDto [L162]
          └─ maps_to UserDto (var User) [L105]
          └─ uses_service UserService
            └─ method GetUserId [L100]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L100]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ sends_request GetProductQuery -> GetProductQueryHandler [L113]
        └─ [web] GET /api/connections/class/funds/{fundCode}  (Workpapers.Next.API.Controllers.Connections.ClassController.GetFund)  [L36–L42] status=200
        └─ [web] GET /api/accounting/ledger/cashflow/audit-report  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowController.GetCashflowAuditReport)  [L29–L40] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetCashflowAuditQuery -> GetCashflowAuditQueryHandler [L38]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowAuditQueryHandler.Handle [L87–L305]
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method Execute [L250]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
              └─ uses_service GetAccountsQuery
                └─ method Execute [L247]
                  └─ resolves_request Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute
                  └─ resolves_request DataGet.Connections.App.Queries.GetAccountsQuery.Execute
                  └─ resolves_request DataGet.Connections.External.Bgl360.Api.Queries.GetAccountsQuery.Execute
                  └─ +4 additional_requests elided
              └─ uses_service GetAccountTypesQuery
                └─ method Execute [L244]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowJournalLinesQuery
                └─ method Execute [L236]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowReconciliationLinesQuery
                └─ method Execute [L233]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowLinesQuery
                └─ method Execute [L226]
                  └─ ... (no implementation details available)
              └─ uses_service GetCashflowCategoriesQuery
                └─ method Execute [L225]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L213]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L122]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/products/by-excel-id/{excelId:int}  (Workpapers.Next.API.Controllers.ProductsController.GetByExcelId)  [L85–L98] status=200
          └─ uses_service UserService
            └─ method GetFirmId [L88]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ sends_request AllProductsForUserQuery -> AllProductsForUserQueryHandler [L88]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.AllProductsForUserQueryHandler.Handle [L31–L65]
              └─ maps_to FirmWithSubscriptionsDto [L49]
                └─ converts_to FirmWithStatsDto [L170]
              └─ maps_to ProductLightDto [L45]
              └─ uses_service UnitOfWork
                └─ method Table [L45]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/ledger/cashflow/reconciliation-lines/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.GetLine)  [L42–L51] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowReconciliationLineDto [L50]
          └─ calls CashflowReconciliationLineRepository.ReadQuery [L45]
          └─ query CashflowReconciliationLine [L45]
            └─ reads_from CashflowReconciliationLines
        └─ [web] GET /workflow/v1/job-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.FullQuery)  [L88–L95] status=200
          └─ calls JobTypeRepository.ReadQuery [L92]
          └─ query JobType [L92]
            └─ reads_from JobTypes
        └─ [web] GET /audit  (Dataverse.Api.Controllers.External.TeamsController.GetAllAudits)  [L39–L44] status=200
          └─ calls TeamRepository.ReadQuery [L43]
          └─ query Team [L43]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L43]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/clients/{id}/children  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClients)  [L147–L159] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ClientSearchLightDto [L150]
            └─ automapper.registration DataverseMappingProfile (Client->ClientSearchLightDto) [L182]
          └─ uses_client ClientRepository [L150]
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L152]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L152]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/ui/trials/{type}/authorisation-url  (Dataverse.Api.Controllers.UI.Trial.TrialsController.GetAuthorisationUrl)  [L46–L57] status=200 [AllowAnonymous]
          └─ uses_service ConnectionService
            └─ method GetApiMethods [L56]
              └─ implementation Dataverse.ApplicationService.Services.ConnectionService.GetApiMethods [L9-L19]
        └─ [web] GET /api/accounting-file/{fileId}/transactions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetTransactions)  [L65–L77] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetTransactionsQuery -> GetTransactionsQueryHandler [L73]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetTransactionsQueryHandler.Handle [L34–L73]
              └─ uses_client DataGetClient [L50]
                └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, method=GetTransactionsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                  └─ target_service DataGet.Api
                    └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetTransactions
              └─ uses_service DataGetClient
                └─ method GetTransactionsAsync [L50]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetTransactionsAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L48]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
        └─ [web] GET /api/stats/binder-type-usage  (Workpapers.Next.API.Controllers.StatsController.BinderTypeUsage)  [L145–L157] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetBinderTypeUsageQuery -> GetBinderTypeUsageQueryHandler [L154]
        └─ [web] GET /api/ui/documents/templates/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplate)  [L89–L98] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTemplateDto [L93]
            └─ automapper.registration DataverseMappingProfile (DocumentTemplate->DocumentTemplateDto) [L432]
          └─ calls DocumentTemplateRepository.ReadQuery [L93]
          └─ query DocumentTemplate [L93]
            └─ reads_from DocumentTemplates
        └─ [web] GET /api/import-runs/compare  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.GetComparison)  [L97–L105] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetCompareImportRunsQuery -> GetCompareImportRunsQueryHandler [L102]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetCompareImportRunsQueryHandler.Handle [L33–L165]
        └─ [web] GET /api/ato/activity-statements  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetActivityStatements)  [L34–L40] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetActivityStatementsQuery -> GetActivityStatementsQueryHandler [L37]
        └─ [web] GET /api/ui/fyi/entities/{entityId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetEntity)  [L150–L156] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method GetEntityAsync [L153]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetEntityAsync [L19-L291]
        └─ [web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CheckForDuplicateSource)  [L540–L560] status=200 [auth=AuthorizationPolicies.User]
          └─ calls SourceRepository.ReadQuery [L548]
          └─ query Source [L548]
          └─ uses_service SourceRepository
            └─ method ReadQuery [L548]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetSubscription)  [L44–L62] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L48]
          └─ uses_client CirrusClient [L60]
            └─ calls GetAll (GET /api/central/storage-accounts) [L131]
              └─ remote_endpoint_expansion_suppressed (see previous expansion)
            └─ calls GetAll (GET /api/central/databases) [L125]
              └─ remote_endpoint_expansion_suppressed (see previous expansion)
            └─ calls CreateFirm (POST /api/central/firms) [L96]
              └─ remote_endpoint_lookup route=/api/central/firms verb=POST
                └─ [web] POST /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.CreateFirm)  [L85–L92] status=201 [auth=Authentication.MachineToMachinePolicy]
                  └─ sends_request CreateFirmCommand -> CreateFirmCommandHandler [L88]
                    └─ handled_by Cirrus.Central.Commands.CreateFirmCommandHandler.Handle [L37–L162]
                      └─ calls CentralRepository (methods: Commit,Add,WriteTable) [L76]
                      └─ uses_service IRequestProcessor (InstancePerDependency)
                        └─ method ProcessAsync [L86]
                          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
                      └─ uses_service TenantService
                        └─ method AssignCurrentTenantId [L78]
                          └─ implementation Cirrus.Central.Tenants.TenantService.AssignCurrentTenantId [L26-L139]
                            └─ uses_service Tenant
                              └─ method AssignCurrentTenantId [L80]
                                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
          └─ calls TenantRepository.ReadTable [L48]
          └─ query Tenant [L48]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L48]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/accounting/reports/templates/{id}/modified-info  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetModifiedInfo)  [L80–L88] status=200 [auth=user]
          └─ maps_to UserUltraLightDto [L85]
            └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
          └─ maps_to ReportTemplateModifiedInfoDto [L83]
            └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateModifiedInfoDto) [L542]
          └─ calls UserRepository.ReadQuery [L85]
          └─ calls ReportTemplateRepository.ReadQuery [L83]
          └─ query User [L85]
          └─ query ReportTemplate [L83]
        └─ [web] GET /api/ui/users/{id:guid}  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUser)  [L78–L85] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetUserQuery -> GetUserQueryHandler [L82]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserQueryHandler.Handle [L22–L40]
              └─ calls UserRepository.ReadQuery [L35]
              └─ maps_to UserDto [L35]
                └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
                └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
        └─ [web] GET /workflow/v1/task-template-groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Get)  [L48–L54] status=200
          └─ maps_to TaskTemplateGroupDto [L51]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
            └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
          └─ calls TaskTemplateGroupRepository.ReadQuery [L51]
          └─ query TaskTemplateGroup [L51]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/workpaperitems/byuser  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForUser)  [L72–L83] status=200
          └─ maps_to WorkpaperItemDto [L76]
          └─ uses_service UserService
            └─ method GetUserId [L77]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L76]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetStandardAccounts)  [L67–L77] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to StandardAccountLightDto [L71]
            └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountLightDto) [L690]
          └─ calls StandardAccountRepository.ReadQuery [L71]
          └─ query StandardAccount [L71]
            └─ reads_from StandardAccounts
          └─ uses_service StandardAccountRepository
            └─ method ReadQuery [L71]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.StandardAccountRepository.ReadQuery (see previous expansion)
        └─ [web] GET /core/v1/teams/audits  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditsQuery)  [L97–L103] status=200
          └─ maps_to EntityAuditDto [L100]
          └─ calls TeamRepository.ReadQuery [L100]
          └─ query Team [L100]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L100]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Get)  [L45–L50] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to SyncServiceDto [L48]
            └─ automapper.registration DataverseMappingProfile (SyncService->SyncServiceDto) [L260]
          └─ calls SyncServiceRepository.ReadQuery [L48]
          └─ query SyncService [L48]
            └─ reads_from SyncServices
        └─ [web] GET /api/ui/imanage/clients  (Dataverse.Api.Controllers.UI.IManageController.GetClients)  [L166–L181] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L169]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L169]
          └─ query IntegrationSettings [L169]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetClientsAsync [L175]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetClientsAsync [L19-L225]
        └─ [web] GET /workpapers/v1/workpaper-records/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsDetailedQuery)  [L76–L82] status=200
          └─ calls WorkpaperRecordRepository.ReadQuery [L79]
          └─ query WorkpaperRecord [L79]
            └─ reads_from WorkpaperRecords
        └─ [web] GET /workpapers/v1/binders/{binderId:Guid}/tax-info  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderTaxInfo)  [L96–L100] status=200
          └─ uses_service IWorkpapersProxyService (AddScoped)
            └─ method Get [L99]
              └─ implementation Workpapers.Next.API.External.Features.WorkpapersProxy.WorkpapersProxyService.Get (see previous expansion)
        └─ [web] GET /api/taxonomy/  (Workpapers.Next.API.Controllers.Workpapers.TaxonomyController.GetAll)  [L32–L39] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to TaxonomyLightDto [L35]
            └─ automapper.registration WorkpapersMappingProfile (Taxonomy->TaxonomyLightDto) [L880]
          └─ calls TaxonomyRepository.ReadQuery [L35]
          └─ query Taxonomy [L35]
            └─ reads_from Taxonomy
        └─ [web] GET /api/ui/users/export/excel/download  (Dataverse.Api.Controllers.UI.Core.UsersController.ExportToExcel)  [L179–L192] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to UserExportProfileDto [L182]
            └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L109]
          └─ calls UserRepository.ReadQuery [L182]
          └─ query User [L182]
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L188]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
          └─ uses_service UserRepository
            └─ method ReadQuery [L182]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L190]
          └─ sends_request ExportUsersToExcelCommand -> ExportUsersToExcelCommandHandler [L189]
        └─ [web] GET /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDisclosureTemplateQuery -> GetDisclosureTemplateQueryHandler [L47]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplateQueryHandler.Handle [L30–L69]
              └─ maps_to DisclosureVariantForDisclosureTemplateDto [L54]
                └─ automapper.registration CirrusMappingProfile (DisclosureVariant->DisclosureVariantForDisclosureTemplateDto) [L850]
              └─ maps_to DisclosureTemplateDto [L47]
                └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateDto) [L826]
              └─ uses_service IControlledRepository<DisclosureVariant> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureVariantRepository.ReadQuery
              └─ uses_service IControlledRepository<DisclosureTemplate> (Scoped (inferred))
                └─ method ReadQuery [L47]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureTemplateRepository.ReadQuery
        └─ [web] GET /workflow/v1/jobs/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditsQuery)  [L104–L110] status=200
          └─ maps_to EntityAuditDto [L107]
          └─ calls JobRepository.ReadQuery [L107]
          └─ query Job [L107]
            └─ reads_from Jobs
        └─ [web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFields)  [L100–L115] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTemplateFieldLightDto [L107]
            └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldLightDto) [L434]
          └─ calls DocumentTemplateFieldRepository.ReadQuery [L107]
          └─ query DocumentTemplateField [L107]
            └─ reads_from DocumentTemplateFields
        └─ [web] GET /api/connections/bgl360/funds/{fundId}/trial-balance/{startDate:datetime}/{endDate:datetime}  (Workpapers.Next.API.Controllers.Connections.Bgl360Controller.GetTrialBalance)  [L41–L47] status=200
        └─ [web] GET /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetSubscriptionByType)  [L50–L56] status=200 [auth=Authentication.UserPolicy]
          └─ calls NotificationSubscriptionRepository.ReadQuery [L54]
          └─ query NotificationSubscription [L54]
            └─ reads_from NotificationSubscriptions
          └─ uses_service UserService
            └─ method GetUserId [L53]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/teams/  (Workpapers.Next.API.Controllers.TeamController.GetTeams)  [L58–L68] status=200
          └─ maps_to TeamDto [L63]
          └─ uses_service UnitOfWork
            └─ method Table [L63]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L61]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
        └─ [web] GET /api/companies-house  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SetConnectionContext)  [L452–L455] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/super/sync-configuration/all  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAllTenants)  [L56–L63] status=200 [auth=Authentication.MasterPolicy]
        └─ [web] GET /api/connections/myob/ar/files  (Workpapers.Next.API.Controllers.Connections.MYOBAccountRightController.GetAccountingFiles)  [L28–L34] status=200
        └─ [web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetEligibleSourceAccounts)  [L69–L89] status=200 [auth=user]
          └─ maps_to SourceAccountLightDto [L80]
            └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
            └─ converts_to LinkedSourceAccount [L72]
            └─ converts_to SourceAccountInJournalModel [L269]
            └─ converts_to MappingSourceAccountModel [L830]
          └─ calls SourceAccountRepository.ReadQuery [L80]
          └─ calls DatasetRepository.ReadQuery [L73]
          └─ query SourceAccount [L80]
            └─ reads_from SourceAccounts
          └─ query Dataset [L73]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L72]
        └─ [web] GET /api/accounting/reports/templates/{fileId:Guid}/default/{masterId:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDefaultFromMaster)  [L134–L138] status=200 [auth=user]
          └─ sends_request ReportTemplateFromMasterQuery -> ReportTemplateFromMasterQueryHandler [L137]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplateFromMasterQueryHandler.Handle [L45–L267]
              └─ maps_to HeaderAccountMatchDto [L67]
              └─ uses_service IControlledRepository<File> (Scoped (inferred))
                └─ method ReadQuery [L77]
                  └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L67]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L65]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /core/v1/users/detailed  (Dataverse.Api.External.Controllers.v1.Core.UsersController.FullQuery)  [L102–L108] status=200
          └─ calls UserRepository.ReadQuery [L105]
          └─ query User [L105]
          └─ uses_service UserRepository
            └─ method ReadQuery [L105]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/fyi/documents/{documentId:guid}  (DataGet.Api.Controllers.Integrations.FyiController.GetDocument)  [L111–L120] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.GetAll)  [L33–L43] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaskTemplateDto [L36]
            └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
          └─ calls TaskTemplateRepository.ReadQuery [L36]
          └─ query TaskTemplate [L36]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/internal/Propagator/workflow-tasks  (Dataverse.Api.Controllers.Internal.PropagatorController.GetWorkflowTasks)  [L133–L148] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls TaskRepository.ReadQuery [L136]
          └─ uses_service TaskRepository
            └─ method ReadQuery [L136]
              └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/workpaperitems/byworkbook/{workbookId}/detailed  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorkbookDetailed)  [L107–L117] status=200
          └─ maps_to WorkpaperItemWithConversationDto [L110]
          └─ uses_service UnitOfWork
            └─ method Table [L110]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/connections/class/funds  (Workpapers.Next.API.Controllers.Connections.ClassController.GetFunds)  [L27–L33] status=200
        └─ [web] GET /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Get)  [L50–L79] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DisclosureVariantDto (var dto) [L71]
          └─ calls ReportContentRepository.LoadReadProperties [L68]
          └─ calls DisclosureVariantRepository.ReadQuery [L56]
          └─ query DisclosureVariant [L56]
            └─ reads_from DisclosureVariants
          └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
            └─ method LoadReadProperties [L68]
              └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties (see previous expansion)
          └─ sends_request PrepareImagesContentCommand [L72]
        └─ [web] GET /api/super/users/getAll  (Dataverse.Api.Controllers.Super.Core.UsersController.GetAll)  [L104–L113] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to UserLightDto [L108]
            └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
          └─ calls UserRepository.ReadQuery [L108]
          └─ query User [L108]
          └─ uses_service UserRepository
            └─ method ReadQuery [L108]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/ui/documents/types/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetDocument)  [L50–L56] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTypeDto [L53]
            └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L420]
          └─ calls DocumentTypeRepository.ReadQuery [L53]
          └─ query DocumentType [L53]
            └─ reads_from DocumentTypes
        └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
        └─ [web] GET /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetPolicyQuery -> GetPolicyQueryHandler [L47]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPolicyQueryHandler.Handle [L30–L68]
              └─ maps_to PolicyVariantForPolicyDto [L54]
                └─ automapper.registration CirrusMappingProfile (PolicyVariant->PolicyVariantForPolicyDto) [L811]
              └─ maps_to PolicyDto [L47]
                └─ automapper.registration CirrusMappingProfile (Policy->PolicyDto) [L789]
              └─ uses_service IControlledRepository<PolicyVariant> (Scoped (inferred))
                └─ method ReadQuery [L54]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyVariantRepository.ReadQuery
              └─ uses_service IControlledRepository<Policy> (Scoped (inferred))
                └─ method ReadQuery [L47]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyRepository.ReadQuery
        └─ [web] GET /api/master-accounts/{id:int}  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Get)  [L47–L55] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ maps_to MasterAccountDto [L50]
            └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
          └─ calls MasterAccountRepository.ReadQuery [L50]
          └─ query MasterAccount [L50]
            └─ reads_from MasterAccounts
          └─ uses_service MasterAccountRepository
            └─ method ReadQuery [L50]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.MasterAccountRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Get)  [L52–L58] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardChartDto [L55]
            └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
            └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
          └─ calls StandardChartRepository.ReadQuery [L55]
          └─ query StandardChart [L55]
            └─ reads_from StandardCharts
        └─ [web] GET /workflow/v1/task-template-groups/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.FullQuery)  [L85–L92] status=200
          └─ calls TaskTemplateGroupRepository.ReadQuery [L89]
          └─ query TaskTemplateGroup [L89]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/ui/notification-subscriptions  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetAllSubscription)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
          └─ calls NotificationSubscriptionRepository.ReadQuery [L43]
          └─ query NotificationSubscription [L43]
            └─ reads_from NotificationSubscriptions
          └─ uses_service UserService
            └─ method GetUserId [L42]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/dataverse/{entityRoute}/audit-entity/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.DataverseController.GetAudit)  [L102–L110] status=200 [auth=Authentication.RequireTenantIdPolicy]
        └─ [web] GET /core/v1/clients/entities/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntity)  [L73–L79] status=200
          └─ maps_to ClientEntityDto [L76]
            └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
          └─ uses_client ClientRepository [L76]
        └─ [web] GET /api/accounting/ledger/accounts/{fileId:Guid}/hierarchy  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAccountHierarchy)  [L162–L187] status=200 [auth=user]
          └─ calls FileRepository.ReadQuery [L167]
          └─ query File [L167]
            └─ reads_from Files
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L165]
        └─ [web] GET /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetById)  [L58–L65] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentVersionDto [L62]
            └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L424]
          └─ calls DocumentVersionRepository.ReadQuery [L62]
          └─ query DocumentVersion [L62]
            └─ reads_from DocumentVersions
        └─ [web] GET /api/ato/gov-link/profile-compare  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshProfileComparison)  [L180–L187] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetIndividualIncomeTaxReturnProfileCompareQuery -> GetIndividualIncomeTaxReturnProfileCompareQueryHandler [L184]
          └─ returns ProfileCompareDto [L184]
        └─ [web] GET /api/reportsettings/layoutrules/{id:int}  (Workpapers.Next.API.Controllers.Reportance.LayoutRulesController.GetLayoutRule)  [L34–L40] status=200
          └─ maps_to LayoutRuleDto [L39]
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/workspaces  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaces)  [L197–L206] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageWorkspacesQuery -> GetIManageWorkspacesQueryHandler [L205]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspacesQueryHandler.Handle [L25–L43]
              └─ uses_service IManageService
                └─ method GetWorkspaces [L36]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaces [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /documents/v1/documents/  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.MinimalQuery)  [L87–L93] status=200
          └─ maps_to DocumentMinimalDto [L90]
          └─ calls DocumentRepository.ReadQuery [L90]
          └─ query Document [L90]
            └─ reads_from Documents
        └─ [web] GET /api/accounting/reports/masters/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetPackage)  [L72–L76] status=200 [auth=user,admin]
          └─ sends_request ReportMasterPackageQuery -> ReportMasterPackageQueryHandler [L75]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterPackageQueryHandler.Handle [L37–L75]
              └─ maps_to ReportPageLayoutLightDto [L68]
                └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
              └─ maps_to FooterTemplateDto [L65]
                └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
                └─ converts_to FooterContentDto [L49]
              └─ maps_to HeaderTemplateLightDto [L62]
                └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
              └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
                └─ method ReadQuery [L68]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
              └─ uses_service IControlledRepository<FooterTemplate> (Scoped (inferred))
                └─ method ReadQuery [L65]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.FooterTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<HeaderTemplate> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.HeaderTemplateRepository.ReadQuery
              └─ uses_service IJurisdictionService (AddScoped)
                └─ method GetFirmJurisdictions [L58]
                  └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetFirmJurisdictions (see previous expansion)
        └─ [web] GET /api/accounting/ledger/accounts/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Get)  [L68–L74] status=200 [auth=user]
          └─ maps_to AccountDto [L71]
            └─ automapper.registration CirrusMappingProfile (Account->AccountDto) [L327]
          └─ calls AccountRepository.ReadQuery [L71]
          └─ query Account [L71]
        └─ [web] GET /api/dataverse/binders  (Workpapers.Next.API.Controllers.DataverseController.GetBinders)  [L352–L361] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
          └─ maps_to BinderListItemDto [L357]
            └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemDto) [L431]
          └─ calls BinderRepository.ReadQuery [L357]
          └─ query Binder [L357]
            └─ reads_from Binders
        └─ [web] GET /api/accounting/assets/reports/monthly/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetMonthlyDepreciationReportExcel)  [L142–L148] status=200 [auth=user]
          └─ sends_request GetExportAssetReportQuery -> GetExportAssetReportQueryHandler [L146]
        └─ [web] GET /api/accounting/files/summary  (Cirrus.Api.Controllers.Accounting.FilesController.GetSummaries)  [L167–L180] status=200 [auth=user]
          └─ maps_to FileUltraLightDto [L170]
            └─ automapper.registration CirrusMappingProfile (File->FileUltraLightDto) [L194]
          └─ calls FileRepository.ReadQuery [L170]
          └─ query File [L170]
            └─ reads_from Files
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L177]
        └─ [web] GET /api/workpapers/{id:Guid}  (Workpapers.Next.API.Controllers.WorkpapersController.Get)  [L45–L49] status=200
          └─ sends_request GetWorkpaperQuery -> GetWorkpaperQueryHandler [L48]
            └─ handled_by Workpapers.Next.Data.Queries.Workpapers.GetWorkpaperQueryHandler.Handle [L30–L69]
              └─ maps_to WorkpaperDto [L43]
              └─ uses_service UnitOfWork
                └─ method Table [L43]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/reports/saved-reports/{id}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.Get)  [L53–L64] status=200 [auth=user]
          └─ maps_to SavedReportFileDto [L63]
          └─ calls PublishedReportFileRepository.ReadQuery [L57]
          └─ query PublishedReportFile [L57]
            └─ reads_from PublishedReportFiles
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L61]
        └─ [web] GET /api/accounting/ledger/journals/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.Get)  [L48–L55] status=200 [auth=user]
          └─ maps_to JournalDto [L51]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L53]
        └─ [web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.MapDataverseIds)  [L581–L627] status=200
          └─ calls UserRepository.ReadQuery [L606]
          └─ query User [L606]
          └─ query BinderStatus [L585]
            └─ reads_from BinderStatuss
          └─ uses_service UserRepository
            └─ method ReadQuery [L606]
              └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery (see previous expansion)
          └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
            └─ method ReadQuery [L585]
              └─ ... (no implementation details available)
        └─ [web] GET /api/tenant/databases/  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetAll)  [L33–L43] status=200 [auth=master]
          └─ maps_to DatabaseLightDto [L36]
          └─ calls TenantRepository.ReadTable [L36]
          └─ query Tenant [L36]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L36]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}/mapping-codes/{mappingCodeId:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCode)  [L51–L55] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodeQuery -> GetExternalReportingSystemMappingCodeQueryHandler [L54]
        └─ [web] GET /api/offices/byfirm/{firmId:Guid}  (Workpapers.Next.API.Controllers.OfficeController.GetOfficesForFirm)  [L45–L56] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to OfficeLightDto [L49]
          └─ uses_service UnitOfWork
            └─ method Table [L49]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/accounting/tax/taxonomy/  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetAll)  [L36–L45] status=200 [auth=user]
          └─ maps_to TaxonomyLightDto [L39]
          └─ uses_service IReadRepository (InstancePerLifetimeScope)
            └─ method Table [L39]
              └─ ... (no implementation details available)
        └─ [web] GET /api/super/tenant-reports/region-report/{regionId:int}  (Dataverse.Api.Controllers.Super.TenantReportsController.GetRegionReport)  [L48–L62] status=200 [auth=Authentication.MasterPolicy]
          └─ sends_request RegionReportBaseQuery [L60]
        └─ [web] GET /api/internal/sync-configuration/  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetAll)  [L44–L50] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SyncConfigurationInsecureDto [L47]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
          └─ calls SyncConfigurationRepository.ReadQuery [L47]
          └─ query SyncConfiguration [L47]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/workpaper-records  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetLinkedSourceAccountIds)  [L519–L560] status=200 [auth=AuthorizationPolicies.User]
          └─ calls SourceAccountRepository.ReadQuery [L529]
          └─ calls BinderRepository.ReadQuery [L524]
          └─ query SourceAccount [L529]
            └─ reads_from SourceAccounts
          └─ query Binder [L524]
            └─ reads_from Binders
          └─ uses_service SourceAccountRepository
            └─ method ReadQuery [L529]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/templates  (Workpapers.Next.API.Controllers.Templates.TemplatesController.UserAuthorisedForTemplate)  [L141–L156] status=200
          └─ uses_service UserService
            └─ method IsSuperUser [L151]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L143]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /workflow/v1/deliverables/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.FullQuery)  [L89–L97] status=200
          └─ calls DeliverableRepository.ReadQuery [L93]
          └─ query Deliverable [L93]
            └─ reads_from Deliverables
        └─ [web] GET /api/ui/users/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUserAudit)  [L194–L219] status=200 [auth=Authentication.UserPolicy]
          └─ calls UserRepository.ReadQuery [L197]
          └─ query User [L197]
          └─ uses_service UserRepository
            └─ method ReadQuery [L197]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/workpapers/starters/{id}/download  (Cirrus.Api.Controllers.Accounting.WorkpapersController.DownloadStarter)  [L101–L106] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service WorkpapersService
            └─ method Get [L105]
              └─ implementation Cirrus.Central.Features.MachineToMachine.WorkpapersService.Get (see previous expansion)
          └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L104]
        └─ [web] GET /core/v1/users/audits  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditsQuery)  [L115–L121] status=200
          └─ maps_to EntityAuditDto [L118]
          └─ calls UserRepository.ReadQuery [L118]
          └─ query User [L118]
          └─ uses_service UserRepository
            └─ method ReadQuery [L118]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Get)  [L47–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to OfficeDto [L50]
            └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
            └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
          └─ calls OfficeRepository.ReadQuery [L50]
          └─ query Office [L50]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L50]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/stats/starterusage  (Workpapers.Next.API.Controllers.StatsController.StarterUsageAllFirms)  [L174–L186] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetStarterUsageQuery -> GetStarterUsageQueryHandler [L183]
        └─ [web] GET /api/workpaper-record-template-notes/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.GetNote)  [L53–L60] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ maps_to WorkpaperRecordTemplateNoteDto [L56]
          └─ uses_service UnitOfWork
            └─ method Table [L56]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntityBeneficialOwner)  [L264–L272] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscCorporateEntityBeneficialOwnerQuery -> GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler [L269]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler.Handle [L19–L32]
        └─ [web] GET /api/matter-text-templates/for-workpaper-record-template/{workpaperRecordTemplateId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetForWorkpaperRecordTemplate)  [L111–L128] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetMatterTextTemplatesWithWorkpaperRecordTemplateQuery [L123]
        └─ [web] GET /ledger/v1/files/{fileId:Guid}/entities  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileEntities)  [L95–L104] status=200
          └─ maps_to EntityReferenceDto [L98]
          └─ calls FileRepository.ReadQuery [L98]
          └─ query File [L98]
            └─ reads_from Files
        └─ [web] GET /api/fyi/documents/{documentVersionId:guid}/downloadUrl  (DataGet.Api.Controllers.Integrations.FyiController.GetDocumentDownloadUrl)  [L133–L142] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentDownloadUrlQuery -> GetDocumentDownloadUrlQueryHandler [L138]
            └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentDownloadUrlQueryHandler.Handle [L18–L33]
              └─ uses_service FyiService
                └─ method GetDocumentDownloadUrl [L29]
                  └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetDocumentDownloadUrl [L30-L166]
                    └─ uses_client FyiHttpClient [L50]
                    └─ uses_service FyiHttpClient
                      └─ method GetCabinets [L50]
                        └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets (see previous expansion)
        └─ [web] GET /api/files/upload  (Workpapers.Next.API.Controllers.Templates.FilesController.UploadFileGet)  [L25–L32] status=200
        └─ [web] GET /api/ui/visualisations/heatmap/find/ledgerfile  (Dataverse.Api.Controllers.UI.Visualisations.HeatMapController.FindCirrusFileHeatMap)  [L58–L62] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/sources/transactions  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTransactions)  [L493–L529] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L498]
          └─ query Binder [L498]
            └─ reads_from Binders
          └─ sends_request GetGeneralLedgerBySourceAccountQuery -> GetGeneralLedgerBySourceAccountQueryHandler [L518]
        └─ [web] GET /api/matters/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MattersController.Get)  [L63–L73] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L70]
          └─ sends_request FindMatterByIdQuery [L66]
        └─ [web] GET /api/companies-house-gateway/document  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetDocumentAsync)  [L90–L94] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/ui/clients/{id}/audit  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientAudit)  [L209–L220] status=200 [auth=Authentication.UserPolicy]
          └─ uses_client ClientRepository [L214]
          └─ sends_request CanIAccessClientQuery -> CanIAccessClientQueryHandler [L212]
        └─ [web] GET /api/fyi-elite/test-connection  (DataGet.Api.Controllers.Integrations.FyiEliteController.TestConnection)  [L92–L101] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/accounting/matching-rules/{id}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Get)  [L61–L78] status=200 [auth=user]
          └─ maps_to MatchingRuleDto [L64]
            └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
          └─ calls MatchingRuleRepository.ReadQuery [L64]
          └─ query MatchingRule [L64]
            └─ reads_from MatchingRules
        └─ [web] GET /api/ui/integration-settings/{apiType}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Get)  [L48–L56] status=200 [auth=Authentication.AdminPolicy]
          └─ maps_to IntegrationSettingsDto [L51]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L51]
          └─ query IntegrationSettings [L51]
            └─ reads_from IntegrationSettingss
        └─ [web] GET /api/accounting/datasets/{id}/preview-rollover-journal  (Cirrus.Api.Controllers.Accounting.DatasetsController.DraftRollover)  [L196–L202] status=200 [auth=user]
          └─ calls DatasetRepository.ReadQuery [L199]
          └─ query Dataset [L199]
          └─ sends_request DraftRolledOverOpeningBalances [L201]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L200]
        └─ [web] GET /core/v1/clients/groups/detailed  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupDetailedQuery)  [L207–L215] status=200
          └─ uses_client ClientRepository [L210]
        └─ [web] GET /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetAll)  [L84–L104] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TeamLightDto [L98]
            └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L150]
          └─ calls TeamRepository.ReadQuery [L98]
          └─ query Team [L98]
            └─ reads_from Teams
          └─ uses_service TeamRepository
            └─ method ReadQuery [L98]
              └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery (see previous expansion)
          └─ uses_service UserService
            └─ method IsInRole [L90]
              └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole (see previous expansion)
        └─ [web] GET /api/hmrc/feedback  (DataGet.Api.Controllers.Integrations.HmrcController.GetFraudHeaderFeedback)  [L79–L89] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service HmrcApiService (AddScoped)
            └─ method GetFraudHeaderFeedback [L84]
              └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.GetFraudHeaderFeedback [L17-L78]
        └─ [web] GET /api/sources/{type}/api-features  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetApiFeatures)  [L478–L484] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetFeatures [L481]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetFeatures [L20-L75]
        └─ [web] GET /api/stats/firm-preferences  (Workpapers.Next.API.Controllers.StatsController.FirmPreferences)  [L252–L263] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=FirmPreferences) [L255]
          └─ sends_request GetFirmPreferencesStatsQuery -> GetFirmPreferencesStatsQueryHandler [L259]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmPreferencesStatsQueryHandler.Handle [L39–L213]
              └─ uses_service List<FirmPreferenceStat>
                └─ method Add [L174]
                  └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.Add [L60-L77]
                    └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                    └─ uses_service IRequestProcessor (InstancePerDependency)
                      └─ method ProcessAsync [L70]
                        └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
                    └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                      └─ method ReadQuery [L66]
                        └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                    └─ query PublishedReportBatch [L66]
                    └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
                    └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
              └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
                └─ method ReadQuery [L136]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
              └─ uses_service UnitOfWork
                └─ method Table [L68]
                  └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/ui/account-mapping/benchmark-code-sets/benchmark-codes  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.GetBenchmarkCodes)  [L46–L53] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetBenchmarkCodesQuery -> GetBenchmarkCodesQueryHandler [L52]
            └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodes.GetBenchmarkCodesQueryHandler.Handle [L30–L60]
              └─ maps_to BenchmarkCodeDto [L55]
              └─ uses_service IControlledRepository<BenchmarkCode> (Scoped (inferred))
                └─ method ReadQuery [L43]
                  └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeRepository.ReadQuery
        └─ [web] GET /core/v1/clients/groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroup)  [L155–L161] status=200
          └─ maps_to ClientGroupDto [L158]
            └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupDto) [L104]
          └─ uses_client ClientRepository [L158]
        └─ [web] GET /api/companies-house/search/dissolved-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDissolvedCompanies)  [L81–L91] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchDissolvedCompaniesQuery -> SearchDissolvedCompaniesQueryHandler [L88]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDissolvedCompaniesQueryHandler.Handle [L27–L40]
        └─ [web] GET /api/accounting/ledger/accounttypes/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.Get)  [L41–L47] status=200 [auth=user]
          └─ maps_to AccountTypeLightDto [L44]
            └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeLightDto) [L246]
          └─ calls AccountTypeRepository.ReadQuery [L44]
          └─ query AccountType [L44]
            └─ reads_from AccountTypes
        └─ [web] GET /api/ui/fyi/verify-connection  (Dataverse.Api.Controllers.UI.FyiController.VerifyConnection)  [L214–L221] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
          └─ uses_service IDatagetFyiService (AddTransient)
            └─ method VerifyConnection [L218]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.VerifyConnection [L19-L291]
        └─ [web] GET /api/accounting/ledger/auto-journals/distributions/{datasetId:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.DistributionController.Get)  [L47–L59] status=200 [auth=user]
          └─ maps_to DistributionDto [L52]
            └─ automapper.registration CirrusMappingProfile (Distribution->DistributionDto) [L498]
          └─ calls DistributionRepository.ReadQuery [L52]
          └─ query Distribution [L52]
            └─ reads_from Distributions
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L50]
        └─ [web] GET /workpapers/v1/matters/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersDetailedQuery)  [L90–L97] status=200
          └─ calls MatterRepository.ReadQuery [L93]
          └─ query Matter [L93]
            └─ reads_from Matters
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValues)  [L52–L66] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to KeyValueDto [L57]
          └─ calls WorksheetRepository.ReadQuery [L57]
          └─ query Worksheet [L57]
            └─ reads_from Worksheets
          └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L55]
        └─ [web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateField)  [L117–L132] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTemplateFieldDto [L125]
            └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldDto) [L435]
          └─ calls DocumentTemplateFieldRepository.ReadQuery [L125]
          └─ query DocumentTemplateField [L125]
            └─ reads_from DocumentTemplateFields
        └─ [web] GET /api/accounting/taxforms/{formId}/balances/{datasetId}  (Cirrus.Api.Controllers.Accounting.TaxFormsController.GetFormBalances)  [L40–L45] status=200 [auth=user]
          └─ sends_request GetTaxLabelBalancesQuery -> GetTaxLabelBalancesQueryHandler [L44]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L43]
        └─ [web] GET /webhooks/v1/webhooks/available  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.GetAvailableWebhooks)  [L69–L73] status=200 [auth=Authentication.CoreRead]
        └─ [web] GET /api/super/sync-configuration/type/{type}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAllByType)  [L71–L83] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SyncConfigurationUltraLightDto [L76]
            └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L265]
          └─ calls SyncConfigurationRepository.ReadQuery [L76]
          └─ query SyncConfiguration [L76]
            └─ reads_from SyncConfigurations
        └─ [web] GET /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UserAuthorisedForTemplate)  [L161–L176] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service UserService
            └─ method IsSuperUser [L171]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L163]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.GetAll)  [L55–L62] status=200 [auth=user]
          └─ maps_to TeamLightDto [L58]
            └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
          └─ calls TeamRepository.ReadQuery [L58]
          └─ query Team [L58]
            └─ reads_from Teams
        └─ [web] GET /api/clients/{id}/entities  (Cirrus.Api.Controllers.Firm.ClientsController.GetEntitiesAndFiles)  [L120–L156] status=200 [auth=user]
          └─ maps_to FileDto [L145]
            └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
            └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
          └─ maps_to EntityWithFileDto [L138]
            └─ automapper.registration CirrusMappingProfile (Entity->EntityWithFileDto) [L172]
          └─ calls FileRepository.ReadQuery [L145]
          └─ calls EntityRepository.ReadQuery [L138]
          └─ calls ClientRepository.ReadQuery [L132]
          └─ query File [L145]
            └─ reads_from Files
          └─ query Entity [L138]
          └─ query Client [L132]
        └─ [web] GET /api/connections/reportance/clientdetails/from-dataset  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetClientDetails)  [L52–L58] status=200
          └─ sends_request GetClientDetailsQuery -> GetClientDetailsQueryHandler [L55]
            └─ handled_by Workpapers.Next.CirrusServices.Queries.GetClientDetailsQueryHandler.Handle [L42–L117]
              └─ uses_service CirrusHttp
                └─ method GetHttpResponseAsync [L64]
                  └─ ... (no implementation details available)
              └─ uses_service CirrusConfig
                └─ method GetBaseUrl [L63]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.Get)  [L93–L105] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorksheetDto [L96]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L102]
        └─ [web] GET /api/ato/gov-link/activity-statements/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshActivityStatementSummary)  [L127–L134] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
          └─ sends_request GetActivityStatementSummaryQuery -> GetActivityStatementSummaryQueryHandler [L131]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementSummaryQueryHandler.Handle [L31–L64]
              └─ uses_client DataGetClient [L58]
                └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy] (see previous expansion)
              └─ uses_service DataGetClient
                └─ method GetActivityStatementSummaryAsync [L58]
                  └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementSummaryAsync [L32-L506]
                    └─ calls GetAccountingFiles [L52]
                    └─ calls GetAccounts [L65]
                    └─ calls GetMovements [L93]
                    └─ calls GetTransactions [L116]
                    └─ calls PostJournal [L127]
                    └─ calls DeleteJournal [L141]
                    └─ calls GetCreditors [L156]
                    └─ calls GetDebtors [L171]
                    └─ calls GetWages [L189]
                    └─ calls GetWages [L190]
                    └─ calls GetWages [L191]
                    └─ calls GetWages [L192]
                    └─ calls GetWages [L193]
                    └─ calls GetActivityStatementsDetail [L214]
                    └─ calls GetActivityStatementSummary [L231]
                    └─ calls GetTransactionDetail [L250]
                    └─ calls GetTransactionSummary [L269]
                    └─ calls GetReportSummary [L307]
                    └─ calls GetProfileComparison [L325]
                    └─ calls GetVatPackage [L343]
                    └─ calls GetVatObligations [L358]
                    └─ calls GetVatObligations [L358]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls SubmitVatReturn [L367]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls ValidateFraudHeaders [L377]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetFraudHeaderFeedback [L387]
                    └─ calls GetAuthorisationUrl [L449]
                    └─ calls Disconnect [L459]
                    └─ calls TestConnection [L472]
                    └─ calls StoreExistingToken [L483]
                    └─ calls StoreExistingFileTokens [L493]
                    └─ calls DisconnectFile [L503]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L50]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
        └─ [web] GET /api/ui/workflow/deliverables/find-job/{jobId:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.FindJobDeliverables)  [L51–L62] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DeliverableLightDto [L55]
          └─ calls DeliverableRepository.ReadQuery [L55]
          └─ query Deliverable [L55]
            └─ reads_from Deliverables
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L57]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L57]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service DeliverableRepository
            └─ method ReadQuery [L55]
              └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery (see previous expansion)
        └─ [web] GET /workpapers/v1/binders/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderDetailedQuery)  [L81–L88] status=200
          └─ calls BinderRepository.ReadQuery [L84]
          └─ query Binder [L84]
            └─ reads_from Binders
        └─ [web] GET /ledger/v1/files/{fileId:Guid}/datasets  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileDatasets)  [L80–L87] status=200
          └─ maps_to DatasetDto [L83]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
            └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
          └─ calls DatasetRepository.ReadQuery [L83]
          └─ query Dataset [L83]
        └─ [web] GET /api/matter-templates/for-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.GetAllByWorkpaperRecord)  [L48–L56] status=200 [auth=AuthorizationPolicies.Administrator]
          └─ calls MatterTemplateRepository.ReadQuery [L51]
          └─ query MatterTemplate [L51]
            └─ reads_from MatterTemplates
        └─ [web] GET /api/starters/byproduct/{ProductId:Guid}  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProduct)  [L61–L67] status=200
          └─ maps_to StarterDto [L66]
          └─ uses_service UserService
            └─ method GetUser [L64]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
          └─ sends_request AllStartersForProductQuery -> AllStartersForProductQueryHandler [L64]
        └─ [web] GET /api/accounting/reports/labels/accounts/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.GetReportLabelsByAccount)  [L59–L72] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to AccountWithReportLabelsDto [L66]
            └─ automapper.registration CirrusMappingProfile (Account->AccountWithReportLabelsDto) [L321]
          └─ calls AccountRepository.ReadQuery [L66]
          └─ query Account [L66]
        └─ [web] GET /api/accounting/reports/notes/policies  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.CanIEditPolicy)  [L201–L212] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method IsInRole [L205]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L210]
        └─ [web] GET /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetById)  [L65–L73] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to JobTypeDto [L68]
            └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
            └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
          └─ calls JobTypeRepository.ReadQuery [L68]
          └─ query JobType [L68]
            └─ reads_from JobTypes
        └─ [web] GET /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Get)  [L40–L46] status=200 [auth=user]
          └─ maps_to TeamDto [L43]
            └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
          └─ calls TeamRepository.ReadQuery [L43]
          └─ query Team [L43]
            └─ reads_from Teams
        └─ [web] GET /api/matter-text-templates  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetBinderParams)  [L181–L215] status=200
          └─ calls WorksheetRepository.ReadQuery [L205]
          └─ calls WorkpaperRecordRepository.ReadQuery [L190]
          └─ calls BinderRepository.ReadQuery [L183]
          └─ query Worksheet [L205]
            └─ reads_from Worksheets
          └─ query WorkpaperRecord [L190]
            └─ reads_from WorkpaperRecords
          └─ query Binder [L183]
            └─ reads_from Binders
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/folders/{folderId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetSubFolders)  [L218–L226] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageSubFoldersQuery -> GetIManageSubFoldersQueryHandler [L225]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageSubFoldersQueryHandler.Handle [L21–L37]
              └─ uses_service IManageService
                └─ method GetSubFolders [L32]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetSubFolders [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/ui/tenants/license-summary/trials/requested  (Dataverse.Api.Controllers.UI.TenantController.GetRequestedLicenseSummaries)  [L97–L118] status=200 [auth=Authentication.UserPolicy]
          └─ uses_cache IDistributedCache.GetRecordAsync [read] [L113]
          └─ calls TenantRepository.ReadTable [L101]
          └─ query Tenant [L101]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L101]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L100]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
        └─ [web] GET /core/v1/clients/entities/detailed  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityDetailedQuery)  [L113–L147] status=200
          └─ maps_to ClientEntityDto [L129]
            └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
          └─ uses_client ClientRepository [L129]
          └─ uses_client ClientRepository [L122]
        └─ [web] GET /api/accounting-file/{fileId}/legacy-bas  (DataGet.Api.Controllers.Connections.AccountingFileController.LegacyGetBAS)  [L167–L175] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/cloud-capcha/test-connection  (DataGet.Api.Controllers.Integrations.CloudCapchaController.TestConnection)  [L93–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ uses_service IApiTokenService (InstancePerLifetimeScope)
            └─ method GetTokenAsync [L98]
              └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync (see previous expansion)
        └─ [web] GET /workpapers/v1/binder-types/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.GetBinderTypesQuery)  [L55–L61] status=200
          └─ maps_to BinderTypeDto [L58]
            └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
            └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
          └─ calls BinderTypeRepository.ReadQuery [L58]
          └─ query BinderType [L58]
            └─ reads_from BinderTypes
        └─ [web] GET /api/ui/documents/documents/{id}/can-i-check-in  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CanICheckInDocument)  [L225–L232] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request CanICheckInDocumentQuery -> CanICheckInDocumentQueryHandler [L229]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanICheckInDocumentQueryHandler.Handle [L29–L74]
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L49]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service UserService
                └─ method IsInRole [L47]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole (see previous expansion)
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L228]
        └─ [web] GET /audit/find  (Dataverse.Api.Controllers.External.ContactsController.FindAudit)  [L47–L51] status=200
          └─ calls ContactRepository.ReadQuery [L50]
          └─ query Contact [L50]
            └─ reads_from DVS_Clients
        └─ [web] GET /api/internal/storage/all-sites  (Dataverse.Api.Controllers.Internal.StorageController.GetAllSites)  [L222–L228] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetAllSharePointSitesQuery -> GetAllSharePointSitesQueryHandler [L225]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetAllSharePointSitesQueryHandler.Handle [L33–L189]
              └─ calls TenantRepository.ReadTable [L92]
              └─ maps_to SecureDocumentStoreCredentialsDto [L83]
              └─ uses_service SharePointRestAuthenticationManager
                └─ method GetSharePointCertificate [L111]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/users/  (Cirrus.Api.Controllers.Firm.UsersController.Search)  [L114–L123] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindUsersQuery -> FindUsersQueryHandler [L122]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindUsersQueryHandler.Handle [L60–L143]
              └─ uses_service IControlledRepository<User> (Scoped (inferred))
                └─ method ReadQuery [L74]
                  └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
        └─ [web] GET /api/sources/{type}/ledger/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedgerForWorksheet)  [L404–L431] status=200 [auth=AuthorizationPolicies.User]
          └─ calls WorkpaperRecordRepository.ReadQuery [L417]
          └─ query WorkpaperRecord [L417]
            └─ reads_from WorkpaperRecords
        └─ [web] GET /api/ui/workflow/reports/find/detail/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.WorkflowReportsController.GetReportDetail)  [L49–L69] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request FindJobsReportDataDetailQuery [L68]
        └─ [web] GET /api/companies-house/company/{companyNumber}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyProfile)  [L119–L127] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyProfileQuery -> GetCompanyProfileQueryHandler [L124]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyProfileQueryHandler.Handle [L15–L25]
        └─ [web] GET /api/internal/job-statuses/audit  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.GetAll)  [L45–L49] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ calls JobStatusRepository.ReadQuery [L48]
          └─ query JobStatus [L48]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Get)  [L50–L56] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to ClientDto [L53]
            └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
          └─ uses_client ClientRepository [L53]
        └─ [web] GET /workpapers/v1/source-accounts/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsMinimalQuery)  [L57–L63] status=200
          └─ calls SourceAccountRepository.ReadQuery [L60]
          └─ query SourceAccount [L60]
            └─ reads_from SourceAccounts
          └─ uses_service SourceAccountRepository
            └─ method ReadQuery [L60]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/assets/reports/monthly  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetMonthlyDepreciationReport)  [L123–L137] status=200 [auth=user]
          └─ sends_request GetMonthlyDepreciationQuery -> GetMonthlyDepreciationQueryHandler [L128]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetMonthlyDepreciationQueryHandler.Handle [L19–L183]
              └─ uses_service IControlledRepository<DepreciationYear> (Scoped (inferred))
                └─ method ReadQuery [L38]
                  └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationYearRepository.ReadQuery
              └─ uses_service GetDepreciationDetailForMonthlyQuery
                └─ method Execute [L37]
                  └─ ... (no implementation details available)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L126]
        └─ [web] GET /api/accounting/reports/notes/disclosure-templates  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.CanIEditDisclosureTemplate)  [L189–L200] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service IUserService (InstancePerLifetimeScope)
            └─ method IsInRole [L193]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L198]
        └─ [web] GET /workflow/v1/jobs/  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.MinimalQuery)  [L69–L77] status=200
          └─ calls JobRepository.ReadQuery [L73]
          └─ query Job [L73]
            └─ reads_from Jobs
        └─ [web] GET /api/super/workflow/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workflow.WorkflowsSubscriptionController.GetSubscription)  [L42–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to SubscriptionDto [L47]
          └─ calls TenantRepository.ReadTable [L47]
          └─ query Tenant [L47]
            └─ reads_from Tenants
          └─ uses_service TenantRepository
            └─ method ReadTable [L47]
              └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/accounting/reports/images  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.GetAll)  [L31–L38] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/workpaperitems/byworkbook/{workbookId}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorkbook)  [L95–L105] status=200
          └─ maps_to WorkpaperItemDto [L98]
          └─ uses_service UnitOfWork
            └─ method Table [L98]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/binders/exist-for-firm  (Workpapers.Next.API.Controllers.Workpapers.BindersController.CheckForBinders)  [L127–L135] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L131]
          └─ query Binder [L131]
            └─ reads_from Binders
        └─ [web] GET /api/message-notifications/  (Workpapers.Next.API.Controllers.Workpapers.MessageNotificationController.GetAll)  [L26–L32] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetMessageNotificationsQuery -> GetMessageNotificationsQueryHandler [L29]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.MessageNotification.GetMessageNotificationsQueryHandler.Handle [L40–L129]
              └─ calls UserRepository.ReadQuery [L110]
              └─ maps_to UserUltraLightDto [L116]
              └─ uses_service DataverseService
                └─ method Get [L70]
                  └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Get [L17-L127]
                    └─ uses_service TenantIdentificationService
                      └─ method GetStandardQueryParams [L70]
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams (see previous expansion)
              └─ uses_service UserService
                └─ method GetUser [L66]
                  └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentTenant [L65]
                  └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
        └─ [web] GET /api/stats/topfirms  (Workpapers.Next.API.Controllers.StatsController.TopFirms)  [L228–L239] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TopFirms-type{*}-period{*}) [L231]
          └─ sends_request GetTopFirmsQuery -> GetTopFirmsQueryHandler [L235]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopFirmsQueryHandler.Handle [L31–L105]
              └─ uses_client KeenClient [L84]
              └─ uses_service UnitOfWork
                └─ method Table [L93]
                  └─ implementation UnitOfWork.Table (see previous expansion)
              └─ uses_service KeenQueryBuilder
                └─ method Build [L85]
                  └─ ... (no implementation details available)
              └─ uses_service KeenClient
                └─ method QueryAsync [L84]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/accounting/reports/templates/{templateId:guid}/download-data  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.DownloadData)  [L241–L260] status=200 [auth=user]
          └─ sends_request ExportReportAsStreamCommand -> ExportReportAsStreamCommandHandler [L250]
            └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportTemplates.ExportReportAsStreamCommandHandler.Handle [L48–L147]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L66]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L246]
        └─ [web] GET /api/ato/individual-income-tax-returns/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetIndividualIncomeTaxReturnSummary)  [L82–L88] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetIndividualIncomeTaxReturnSummaryQuery -> GetIndividualIncomeTaxReturnSummaryQueryHandler [L85]
        └─ [web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetUpdateBinderParams)  [L518–L539] status=200
          └─ calls BinderColumnDataRepository.WriteQuery [L531]
          └─ write BinderColumnData [L531]
            └─ reads_from BinderColumnData
          └─ query BinderStatus [L523]
            └─ reads_from BinderStatuss
          └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
            └─ method ReadQuery [L523]
              └─ ... (no implementation details available)
        └─ [web] GET /api/ato/activity-statements/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetActivityStatementSummary)  [L42–L48] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetActivityStatementSummaryQuery -> GetActivityStatementSummaryQueryHandler [L45]
        └─ [web] GET /workpapers/v1/source-accounts/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsDetailedQuery)  [L75–L81] status=200
          └─ calls SourceAccountRepository.ReadQuery [L78]
          └─ query SourceAccount [L78]
            └─ reads_from SourceAccounts
          └─ uses_service SourceAccountRepository
            └─ method ReadQuery [L78]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/hmrc  (DataGet.Api.Controllers.Integrations.HmrcController.SetConnectionContext)  [L91–L94] status=200 [auth=Authentication.MachineToMachinePolicy]
        └─ [web] GET /api/ui/workflow/excel/download  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadJobs)  [L49–L82] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L79]
          └─ sends_request ExportJobsFromExcelCommand -> ExportJobsFromExcelCommandHandler [L78]
            └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ExportJobsFromExcelCommandHandler.Handle [L27–L45]
              └─ uses_service WorkflowExcelWriter
                └─ method WriteAsync [L41]
                  └─ ... (no implementation details available)
          └─ sends_request FindExcelJobsQuery [L65]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividual)  [L294–L302] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscIndividualQuery -> GetCompanyPscIndividualQueryHandler [L299]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualQueryHandler.Handle [L19–L31]
        └─ [web] GET /api/offices/{id}/teams  (Workpapers.Next.API.Controllers.OfficeController.GetOfficeTeams)  [L93–L100] status=200
          └─ maps_to TeamDto [L96]
          └─ uses_service UnitOfWork
            └─ method Table [L96]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/for-binder/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetWorksheetForBinder)  [L116–L124] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to WorksheetUltraLightDto [L120]
            └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetUltraLightDto) [L478]
          └─ calls WorksheetRepository.ReadQuery [L120]
          └─ query Worksheet [L120]
            └─ reads_from Worksheets
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L119]
        └─ [web] GET /api/ui/sources/{type}/authorization-url  (Dataverse.Api.Controllers.UI.SourcesController.GetAuthorizationUrl)  [L25–L30] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/ledger/journals/package  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetPackage)  [L156–L160] status=200 [auth=user]
        └─ [web] GET /api/workpaper-record-templates/global  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GlobalSearch)  [L66–L70] status=200
          └─ sends_request FindWorkpaperRecordTemplatesGlobalQuery [L69]
        └─ [web] GET /api/connections/reportance/datasets/{fileId}  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetDatasets)  [L36–L42] status=200
          └─ sends_request GetDatasetsQuery -> GetDatasetsQueryHandler [L39]
            └─ handled_by Workpapers.Next.CirrusServices.Queries.GetDatasetsQueryHandler.Handle [L21–L37]
              └─ uses_service CirrusConfig
                └─ method GetBaseUrl [L35]
                  └─ ... (no implementation details available)
              └─ uses_service CirrusHttp
                └─ method GetHttpResponseAsync [L35]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/binder-roll-over/range-values  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetRangeValues)  [L51–L62] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service StorageService
            └─ method GetStream [L56]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.GetStream (see previous expansion)
          └─ uses_service TenantService
            └─ method GetCurrentTenant [L54]
              └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
          └─ uses_storage StorageService.GetStream [L56]
        └─ [web] GET /api/journals/  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetJournals)  [L104–L112] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetJournalsQuery -> GetJournalsQueryHandler [L109]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Journals.GetJournalsQueryHandler.Handle [L28–L53]
              └─ uses_service JournalServiceFactory
                └─ method GetService [L49]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L41]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L107]
        └─ [web] GET /api/worksheets/archive-maps  (Workpapers.Next.API.Controllers.WorksheetsController.GetArchivedWorkpaperRecordTemplateMappings)  [L71–L78] status=200
          └─ maps_to ArchivedWorkpaperRecordTemplateMappingLightDto [L74]
            └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingLightDto) [L364]
          └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L74]
          └─ query ArchivedWorkpaperRecordTemplateMapping [L74]
            └─ reads_from ArchivedWorkpaperRecordTemplateMappings
        └─ [web] GET /api/accounting/matching-rules/rule-set/{ruleSetId}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.GetAllMasters)  [L87–L99] status=200 [auth=user,admin]
          └─ maps_to MatchingRuleDto [L90]
            └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
          └─ calls MatchingRuleRepository.ReadQuery [L90]
          └─ query MatchingRule [L90]
            └─ reads_from MatchingRules
        └─ [web] GET /api/accounting/assets/assets/{id}/split-info  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.GetSplitInfo)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to AssetSplitInfoDto [L42]
            └─ automapper.registration CirrusMappingProfile (Asset->AssetSplitInfoDto) [L896]
          └─ calls AssetRepository.ReadQuery [L42]
          └─ query Asset [L42]
            └─ reads_from Assets
        └─ [web] GET /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Get)  [L49–L55] status=200
          └─ maps_to WorkflowTaskDto [L52]
            └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L371]
            └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
          └─ calls TaskRepository.ReadQuery [L52]
          └─ query WorkflowTask [L52]
            └─ reads_from DVS_Tasks
        └─ [web] GET /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetById)  [L49–L57] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to KanbanColumnDto [L52]
            └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
            └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
          └─ calls KanbanColumnRepository.ReadQuery [L52]
          └─ query KanbanColumn [L52]
            └─ reads_from KanbanColumns
        └─ [web] GET /api/reportsettings/policies/withparagraphs  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPoliciesWithParagraphs)  [L94–L108] status=200
          └─ maps_to OrderedPolicy [L107]
            └─ converts_to OrderedPolicy [L17]
          └─ uses_service UserService
            └─ method IsSuperUser [L98]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L104]
        └─ [web] GET /loaderio-e3b65372cc4066290ec67078a9c6cd0a  (Cirrus.Api.Controllers.LoadTestController.LoaderIo)  [L10–L14] status=200 [AllowAnonymous]
        └─ [web] GET /api/ui/clients/{id}/group/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientGroupSummary)  [L161–L177] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ClientSummaryAsParentDto [L164]
            └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryAsParentDto) [L210]
          └─ uses_client ClientRepository [L164]
          └─ calls DocumentRepository.ReadQuery [L171]
          └─ query Document [L171]
            └─ reads_from Documents
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L166]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L166]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /api/internal/storage/process-zip/{contextId}  (Dataverse.Api.Controllers.Internal.StorageController.ProcessZip)  [L144–L150] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
        └─ [web] GET /api/super/users/all/{id:guid}  (Dataverse.Api.Controllers.Super.Core.UsersController.Get)  [L98–L102] status=200 [auth=Authentication.MasterPolicy]
          └─ sends_request GetCentralUserQuery -> GetCentralUserQueryHandler [L101]
            └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetCentralUserQueryHandler.Handle [L32–L144]
              └─ calls TenantRepository (methods: WriteTable,ReadTable) [L77]
              └─ maps_to CentralUserWithTenantsDto [L50]
        └─ [web] GET /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Get)  [L45–L54] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to JobDto [L50]
            └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L307]
            └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
            └─ converts_to JobExportDto [L312]
          └─ calls JobRepository.ReadQuery [L50]
          └─ query Job [L50]
            └─ reads_from Jobs
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L52]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L52]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L48]
        └─ [web] GET /api/accounting/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.RefreshAccountHierarchy)  [L120–L148] status=200 [auth=Authentication.UserPolicy]
          └─ calls StandardAccountRepository.ReadQuery [L138]
          └─ calls MasterAccountRepository.ReadQuery [L135]
          └─ query StandardAccount [L138]
            └─ reads_from StandardAccounts
          └─ query MasterAccount [L135]
            └─ reads_from MasterAccounts
        └─ [web] GET /core/v1/clients/groups/{id:Guid}/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupWithChildren)  [L169–L175] status=200
          └─ maps_to ClientGroupWithChildrenDto [L172]
            └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupWithChildrenDto) [L107]
          └─ uses_client ClientRepository [L172]
        └─ [web] GET /api/connections/hasconnection  (Workpapers.Next.API.Controllers.Connections.ConnectionsController.HasConnection)  [L54–L68] status=200
        └─ [web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.UpdateMatterCounts)  [L629–L636] status=200
        └─ [web] GET /api/account-types/{id:int}  (Workpapers.Next.API.Controllers.AccountTypesController.Get)  [L42–L50] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to AccountTypeDto [L45]
            └─ automapper.registration WorkpapersMappingProfile (AccountType->AccountTypeDto) [L709]
          └─ calls AccountTypeRepository.ReadQuery [L45]
          └─ query AccountType [L45]
            └─ reads_from AccountTypes
          └─ uses_service AccountTypeRepository
            └─ method ReadQuery [L45]
              └─ implementation Workpapers.Next.Data.Repository.Workpapers.AccountTypeRepository.ReadQuery [L8-L38]
        └─ [web] GET /api/accounting/ledger/cashflow/reconciliation-lines/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.GetLines)  [L53–L63] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to CashflowReconciliationLineDto [L58]
            └─ automapper.registration CirrusMappingProfile (CashflowReconciliationLine->CashflowReconciliationLineDto) [L472]
          └─ calls CashflowReconciliationLineRepository.ReadQuery [L58]
          └─ query CashflowReconciliationLine [L58]
            └─ reads_from CashflowReconciliationLines
        └─ [web] GET /api/matters/find  (Workpapers.Next.API.Controllers.Workpapers.MattersController.FindMatters)  [L53–L61] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request FindMattersQuery -> FindMattersQueryHandler [L58]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.Matters.FindMattersQueryHandler.Handle [L49–L188]
              └─ maps_to MatterWithMessagesDto [L120]
                └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
                └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L157]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Matter> (Scoped (inferred))
                └─ method ReadQuery [L67]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterRepository.ReadQuery
        └─ [web] GET /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaskTemplateGroupDto [L48]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
            └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
          └─ calls TaskTemplateGroupRepository.ReadQuery [L48]
          └─ query TaskTemplateGroup [L48]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/stats/usage/{firmId:guid}/template  (Workpapers.Next.API.Controllers.StatsController.GetFirmRecordTemplatesUsage)  [L71–L85] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetRecordTemplatesUsageQuery -> GetRecordTemplatesUsageQueryHandler [L82]
        └─ [web] GET /api/connections/reportance/debtors  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetDebtors)  [L92–L98] status=200
        └─ [web] GET /api/ui/visualisations/heatmap/find/binder  (Dataverse.Api.Controllers.UI.Visualisations.HeatMapController.FindBinderHeatMap)  [L52–L56] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/ledger/standard-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForStandardChart)  [L43–L65] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to StandardAccountLightDto [L54]
            └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightDto) [L445]
          └─ calls StandardAccountRepository.ReadQuery [L54]
          └─ query StandardAccount [L54]
            └─ reads_from StandardAccounts
          └─ sends_request GetDefaultStandardChartIdQuery -> GetDefaultStandardChartIdQueryHandler [L52]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
              └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
                └─ method ReadQuery [L36]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
        └─ [web] GET /api/super/tenants/{tenantId}/sso/open-id/configs/  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.GetAllOpenIdConfig)  [L31–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_service IOpenIdService (AddScoped)
            └─ method GetAllOpenIdConfigAsync [L34]
              └─ implementation Dataverse.Services.Features.SSO.OpenIdService.GetAllOpenIdConfigAsync [L9-L53]
                └─ uses_service IdentityService
                  └─ method AddOpenIdConfigAsync [L23]
                    └─ implementation Dataverse.Services.Features.Identity.IdentityService.AddOpenIdConfigAsync (see previous expansion)
        └─ [web] GET /api/clients/{id:guid}  (Workpapers.Next.API.Controllers.ClientsController.Get)  [L68–L76] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to ClientDto [L73]
            └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
          └─ uses_client ClientRepository [L73]
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.UpdateMatterCounts)  [L448–L455] status=200 [auth=AuthorizationPolicies.User]
        └─ [web] GET /api/ui/account-mapping/mapping-codes/{code}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.GetMappingCodeByCode)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}/verification-state  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualVerificationState)  [L304–L312] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscIndividualVerificationStateQuery -> GetCompanyPscIndividualVerificationStateQueryHandler [L309]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualVerificationStateQueryHandler.Handle [L19–L31]
        └─ [web] GET /api/journals/for-worksheet/{worksheetId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetForWorksheet)  [L142–L158] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderRepository.ReadQuery [L147]
          └─ query Binder [L147]
            └─ reads_from Binders
          └─ uses_service Service
            └─ method GetForWorksheet [L155]
              └─ ... (no implementation details available)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L145]
        └─ [web] GET /api/internal/documents/{id:Guid}/data  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.ReadDocumentData)  [L322–L326] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetDocumentDataQuery -> GetDocumentDataQueryHandler [L325]
        └─ [web] GET /api/gov-link/activity-statements/test/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.MultipleActivityStatementTest)  [L66–L73] status=200 [AllowAnonymous]
          └─ uses_service TestService
            └─ method GetMultipleActivityStatements [L72]
              └─ ... (no implementation details available)
        └─ [web] GET /api/stats/productusage  (Workpapers.Next.API.Controllers.StatsController.ProductUsageAllFirms)  [L116–L128] status=200 [auth=AuthorizationPolicies.SuperUser]
          └─ sends_request GetProductUsageQuery -> GetProductUsageQueryHandler [L125]
        └─ [web] GET /api/accounting/reports/page-layouts/templates  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetTemplates)  [L68–L72] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetReportPageLayoutsQuery [L71]
        └─ [web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}/mapping-codes  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCodes)  [L57–L61] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetExternalReportingSystemMappingCodesQuery -> GetExternalReportingSystemMappingCodesQueryHandler [L60]
        └─ [web] GET /api/matter-templates/download-bulk-upload-template  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.DownloadBulkUploadTemplate)  [L79–L85] status=200 [auth=AuthorizationPolicies.Administrator]
        └─ [web] GET /api/ui/documents/templates/{templateId:guid}/download  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.DownloadTemplateData)  [L282–L288] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDocumentTemplateDataQuery -> GetDocumentTemplateDataQueryHandler [L287]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentTemplateDataQueryHandler.Handle [L30–L95]
              └─ uses_service DocumentServiceFactory
                └─ method GetDocumentWithService [L75]
                  └─ implementation DocumentServiceFactory.GetDocumentWithService (see previous expansion)
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method WriteQuery [L63]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
              └─ uses_service IControlledRepository<DocumentTemplate> (Scoped (inferred))
                └─ method ReadQuery [L50]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentTemplateRepository.ReadQuery
          └─ sends_request CanIAuthorDocumentTemplatesQuery -> CanIAuthorDocumentTemplatesQueryHandler [L285]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
              └─ uses_service UserService
                └─ method GetUserAsync [L29]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync (see previous expansion)
        └─ [web] GET /api/reportsettings/defaultreporttemplate/id  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.GetDefaultReportTemplateId)  [L51–L58] status=200
          └─ uses_service UserService
            └─ method IsSuperUser [L54]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L55]
        └─ [web] GET /api/dataverse/{entityRoute}/audit  (Workpapers.Next.API.Controllers.DataverseController.GetData)  [L367–L379] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
          └─ uses_service TenantService
            └─ method GetCurrentTenant [L374]
              └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
          └─ sends_request DataverseAuditQuery -> DataverseAuditQueryHandler [L376]
            └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseAuditQueryHandler.Handle [L26–L69]
              └─ maps_to DataverseAuditDto [L63]
                └─ automapper.registration CirrusMappingProfile (Entity->DataverseAuditDto) [L178]
              └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
                └─ method ReadQuery [L63]
                  └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
              └─ uses_service IControlledRepository<Client> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
              └─ uses_service IControlledRepository<User> (Scoped (inferred))
                └─ method ReadQuery [L60]
                  └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
              └─ uses_service IControlledRepository<Team> (Scoped (inferred))
                └─ method ReadQuery [L58]
                  └─ implementation Cirrus.Data.Repository.Firm.TeamRepository.ReadQuery
              └─ uses_service IControlledRepository<Office> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
        └─ [web] GET /api/internal/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplateGroupsController.GetById)  [L45–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ maps_to TaskTemplateGroupDto [L48]
            └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
            └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
          └─ calls TaskTemplateGroupRepository.ReadQuery [L48]
          └─ query TaskTemplateGroup [L48]
            └─ reads_from TaskTemplateGroups
        └─ [web] GET /api/reportsettings/policies/{id}/withsettings  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetOrderedPolicyForFirm)  [L58–L76] status=200
          └─ maps_to OrderedPolicy [L75]
            └─ converts_to OrderedPolicy [L17]
          └─ uses_service UnitOfWork
            └─ method Table [L63]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_service UserService
            └─ method GetFirmId [L61]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
        └─ [web] GET /api/workpapers/auto-reconcile/debtors  (Cirrus.Api.Controllers.Workpapers.AutoReconcileController.GetDebtors)  [L37–L43] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetApiMethods [L41]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/reportance/accounts/  (Workpapers.Next.API.Controllers.Reportance.AccountsController.GetAccounts)  [L21–L27] status=200
          └─ uses_service UnitOfWork
            └─ method Table [L24]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /api/internal/users/profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.Profile)  [L100–L109] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
          └─ sends_request GetUserProfileQuery -> GetUserProfileQueryHandler [L103]
        └─ [web] GET /api/accounting/reports/notes/reporting-suites/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetAllUltraLight)  [L57–L75] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ReportingSuiteUltraLightDto [L64]
            └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteUltraLightDto) [L766]
          └─ calls ReportingSuiteRepository.ReadQuery [L64]
          └─ query ReportingSuite [L64]
            └─ reads_from PolicySuites
          └─ uses_service IJurisdictionService (AddScoped)
            └─ method GetUserJurisdictions [L62]
              └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetUserJurisdictions (see previous expansion)
        └─ [web] GET /workflow/v1/task-templates/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.FullQuery)  [L87–L95] status=200
          └─ calls TaskTemplateRepository.ReadQuery [L91]
          └─ query TaskTemplate [L91]
            └─ reads_from TaskTemplates
        └─ [web] GET /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Get)  [L42–L48] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TeamUserDto [L45]
            └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserDto) [L140]
          └─ calls TeamUserRepository.ReadQuery [L45]
          └─ query TeamUser [L45]
            └─ reads_from TeamUsers
        └─ [web] GET /api/template-versions/{id:Guid}/reconciliation-fields  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.TemplateVersionReconciliationFields)  [L84–L93] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to DefaultReconciliationFieldDto [L87]
          └─ uses_service UnitOfWork
            └─ method Table [L87]
              └─ implementation UnitOfWork.Table (see previous expansion)
        └─ [web] GET /workflow/v1/tasks/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.FullQuery)  [L89–L99] status=200
          └─ calls TaskRepository.ReadQuery [L93]
          └─ query WorkflowTask [L93]
            └─ reads_from DVS_Tasks
        └─ [web] GET /api/accounting-file/{fileId}/debtors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors)  [L119–L127] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDebtorsQuery -> GetDebtorsQueryHandler [L123]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetDebtorsQueryHandler.Handle [L25–L56]
              └─ uses_client DataGetClient [L52]
                └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetDebtorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                  └─ target_service DataGet.Api
                    └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors
              └─ uses_service DataGetClient
                └─ method GetDebtorsAsync [L52]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDebtorsAsync [L15-L302]
                    └─ calls GetAuthorisationUrl [L45]
                    └─ calls Disconnect [L55]
                    └─ calls DisconnectFile [L65]
                    └─ calls GetAccountingFiles [L74]
                    └─ calls TestConnection [L84]
                    └─ calls GetSourceDivisions [L95]
                    └─ calls GetAccounts [L106]
                    └─ calls GetMovements [L130]
                    └─ calls GetTransactions [L151]
                    └─ calls PostJournal [L161]
                    └─ calls PostAccount [L171]
                    └─ calls DeleteJournal [L182]
                    └─ calls GetCreditors [L194]
                    └─ calls GetDebtors [L206]
                    └─ calls GetWages [L218]
                    └─ calls StoreExistingToken [L228]
                    └─ calls StoreExistingFileTokens [L238]
                    └─ calls RequestLodgementAsync [L244]
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L45]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L40]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource (see previous expansion)
          └─ returns Debtors [L123]
        └─ [web] GET /api/accounting/datasets/{id}/api-features  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetApiFeatures)  [L76–L84] status=200 [auth=user]
          └─ calls DatasetRepository.ReadQuery [L80]
          └─ query Dataset [L80]
          └─ uses_service ApiService (SingleInstance)
            └─ method GetFeatures [L83]
              └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures (see previous expansion)
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L79]
        └─ [web] GET /api/ui/account-mapping/external-reporting-systems  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystems)  [L45–L50] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetExternalReportingSystemsQuery -> GetExternalReportingSystemsQueryHandler [L49]
        └─ [web] GET /api/sources/{type}/authorization-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAuthorizationUrl)  [L171–L177] status=200 [auth=AuthorizationPolicies.User]
          └─ uses_service UserService
            └─ method GetUserId [L174]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ uses_service IConnectionApiService (AddSingleton)
            └─ method GetApiMethods [L174]
              └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods (see previous expansion)
        └─ [web] GET /api/companies-house/document/{documentId}/download  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.DownloadDocumentContent)  [L418–L430] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentContentQuery -> GetDocumentContentQueryHandler [L423]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentContentQueryHandler.Handle [L15–L31]
        └─ [web] GET /api/audit-trail/search  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.Search)  [L27–L44] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetAuditHistoriesQuery -> GetAuditHistoriesQueryHandler [L40]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.AuditHistories.GetAuditHistoriesQueryHandler.Handle [L28–L184]
              └─ uses_service AuditHistoryStorageService
                └─ method GetLogs [L182]
                  └─ implementation Workpapers.Next.Data.Storage.AuditHistory.AuditHistoryStorageService.GetLogs [L19-L174]
                    └─ uses_service AuditHistoryEntityType[]
                      └─ method BuildPartitionKey [L152]
                        └─ ... (no implementation details available)
                    └─ uses_service TableStorageService
                      └─ method WriteToStorage [L97]
                        └─ implementation Workpapers.Next.Data.Storage.AzureTables.TableStorageService.WriteToStorage [L15-L161]
                          └─ uses_service TenantIdentificationService
                            └─ method WriteBatch [L36]
                              └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.WriteBatch [L15-L131]
                                └─ uses_service RequestProcessor
                                  └─ method GetCatalogByFirmId [L104]
                                    └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                    └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                    └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                                    └─ +1 additional_requests elided
                                └─ uses_service FirmLightDto
                                  └─ method AssignActiveTenant [L77]
                                    └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
                    └─ uses_service IBackgroundTaskQueue (AddSingleton)
                      └─ method CommitLogs [L86]
                        └─ ... (no implementation details available)
                    └─ uses_service IServiceScopeFactory
                      └─ method CommitLogs [L72]
                        └─ ... (no implementation details available)
                    └─ uses_service TenantIdentificationService
                      └─ method CommitLogs [L67]
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.CommitLogs [L15-L131]
                          └─ uses_service RequestProcessor
                            └─ method GetCatalogByFirmId [L104]
                              └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ +1 additional_requests elided
                          └─ uses_service FirmLightDto
                            └─ method AssignActiveTenant [L77]
                              └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
                    └─ uses_service ClaimsPrincipalAccessor
                      └─ method CommitLogs [L66]
                        └─ ... (no implementation details available)
                    └─ uses_service List<AuditHistoryToStorageDto>
                      └─ method QueueLog [L58]
                        └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.QueueLog [L60-L77]
                          └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                          └─ uses_service IRequestProcessor (InstancePerDependency)
                            └─ method ProcessAsync [L70]
                              └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
                          └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                            └─ method ReadQuery [L66]
                              └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                          └─ query PublishedReportBatch [L66]
                          └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
                          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
                    └─ uses_storage TableStorageService.WriteToStorage [L97]
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L142]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
              └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
                └─ method ReadQuery [L101]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L68]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
                └─ method ReadQuery [L56]
                  └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
              └─ uses_storage AuditHistoryStorageService.GetLogs [L182]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L36]
        └─ [web] GET /api/companies-house/search/all  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAll)  [L29–L37] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request SearchAllQuery -> SearchAllQueryHandler [L34]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAllQueryHandler.Handle [L17–L28]
        └─ [web] GET /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetAll)  [L27–L33] status=200
          └─ sends_request GetBinderColumnTemplateSets [L32]
          └─ sends_request GetBinderColumnTemplateSetsLight -> GetBinderColumnTemplateSetsHandler [L31]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetsHandler.Handle [L17–L46]
              └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
                └─ method ReadQuery [L40]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
        └─ [web] GET /api/accounting/workpapers/starters  (Cirrus.Api.Controllers.Accounting.WorkpapersController.GetStarters)  [L68–L95] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service WorkpapersService
            └─ method Get [L76]
              └─ implementation Cirrus.Central.Features.MachineToMachine.WorkpapersService.Get (see previous expansion)
          └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L71]
        └─ [web] GET /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Get)  [L42–L48] status=200 [auth=user]
          └─ maps_to DepreciationYearDto [L45]
            └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
          └─ calls DepreciationYearRepository.ReadQuery [L45]
          └─ query DepreciationYear [L45]
            └─ reads_from DepreciationYears
        └─ [web] GET /api/accounting/reports/layout/template/{pageTypeId:int}/layout  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLayoutController.GetTemplatePageLayoutOptions)  [L57–L61] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/assets/reports/tax-return/non-sbe  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetTaxReturnReportNonSbe)  [L198–L209] status=200 [auth=user]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L201]
        └─ [web] GET /api/ledger-reports/{binderColumnDataId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetTrialBalance)  [L41–L74] status=200 [auth=AuthorizationPolicies.User]
          └─ calls BinderColumnDataRepository.ReadQuery [L50]
          └─ query BinderColumnData [L50]
            └─ reads_from BinderColumnData
          └─ sends_request GetTrialBalanceForDatesQuery -> GetTrialBalanceForDatesQueryHandler [L65]
          └─ sends_request GetTrialBalanceWithAdjustmentsQuery -> GetTrialBalanceWithAdjustmentsQueryHandler [L64]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetTrialBalanceWithAdjustmentsQueryHandler.Handle [L44–L100]
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method ExecuteWithSourceAccountInfoDto [L97]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.ExecuteWithSourceAccountInfoDto
        └─ [web] GET /api/accounting/reports/header-templates/  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.GetAll)  [L54–L60] status=200 [auth=user]
          └─ maps_to HeaderTemplateLightDto [L57]
            └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
          └─ calls HeaderTemplateRepository.ReadQuery [L57]
          └─ query HeaderTemplate [L57]
            └─ reads_from ReportHeaderTemplates
        └─ [web] GET /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Get)  [L77–L81] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetReportingSuiteQuery -> GetReportingSuiteQueryHandler [L80]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetReportingSuiteQueryHandler.Handle [L26–L50]
              └─ maps_to ReportingSuiteDto [L42]
                └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteDto) [L762]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L43]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<ReportingSuite> (Scoped (inferred))
                └─ method ReadQuery [L42]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.ReportingSuiteRepository.ReadQuery
        └─ [web] GET /api/products/{id:Guid}  (Workpapers.Next.API.Controllers.ProductsController.Get)  [L70–L83] status=200
          └─ uses_service UserService
            └─ method GetFirmId [L73]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
          └─ sends_request AllProductsForUserQuery -> AllProductsForUserQueryHandler [L73]
        └─ [web] GET /workpapers/v1/custom-statuses/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.GetStatusesQuery)  [L55–L61] status=200
          └─ maps_to CustomStatusDto [L58]
            └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
            └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
          └─ calls CustomStatusRepository.ReadQuery [L58]
          └─ query CustomStatus [L58]
            └─ reads_from CustomStatuses
        └─ [web] GET /api/central/databases/{databaseId}  (Cirrus.Api.Controllers.Central.DatabaseController.Get)  [L38–L45] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to DatabaseDto [L41]
          └─ calls CentralRepository.ReadTable [L41]
          └─ uses_service CentralRepository
            └─ method ReadTable [L41]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
        └─ [web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscList)  [L374–L384] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyPscListQuery -> GetCompanyPscListQueryHandler [L381]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscListQueryHandler.Handle [L26–L38]
        └─ [web] GET /api/internal/document-audit-trail/  (Dataverse.Api.Controllers.Internal.Documents.DocumentAuditLogsController.GetDocumentAuditLogs)  [L42–L55] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentAuditLogDto [L49]
            └─ automapper.registration DataverseMappingProfile (DocumentAuditLog->DocumentAuditLogDto) [L445]
          └─ calls DocumentAuditLogRepository.ReadQuery [L49]
          └─ query DocumentAuditLog [L49]
            └─ reads_from DVS_DocumentAuditLogs
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L47]
        └─ [web] GET /core/v1/clients/groups  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupMinimalQuery)  [L187–L195] status=200
          └─ uses_client ClientRepository [L190]
        └─ [web] GET /workpapers/v1/worksheets/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsMinimalQuery)  [L59–L65] status=200
          └─ calls WorksheetRepository.ReadQuery [L62]
          └─ query Worksheet [L62]
            └─ reads_from Worksheets
        └─ [web] GET /api/accounting/reports/templates/{id}/disclosures  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDisclosures)  [L124–L128] status=200 [auth=user]
          └─ sends_request GetDefaultDisclosuresForExistingReportQuery -> GetDefaultDisclosuresForExistingReportQueryHandler [L127]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultDisclosuresForExistingReportQueryHandler.Handle [L42–L104]
              └─ calls ReportContentRepository.LoadReadProperties [L70]
              └─ maps_to DisclosureModel [L72]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L85]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
        └─ [web] GET /api/ui/documents/documents/{id}/{versionId}/preview-by-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocumentByVersion)  [L207–L214] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDocumentPreviewLinkByVersionQuery -> GetDocumentPreviewLinkByVersionQueryHandler [L211]
            └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkByVersionQueryHandler.Handle [L35–L135]
              └─ maps_to DocumentVersionLightDto [L125]
                └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionLightDto) [L421]
              └─ maps_to SecureDocumentStoreDto [L86]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L131]
                  └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service DocumentServiceFactory
                └─ method GetForStore [L86]
                  └─ implementation DocumentServiceFactory.GetForStore (see previous expansion)
              └─ uses_service IControlledRepository<Document> (Scoped (inferred))
                └─ method ReadQuery [L77]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
              └─ uses_service string[]
                └─ method Contains [L72]
                  └─ ... (no implementation details available)
              └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
                └─ method WriteQuery [L64]
                  └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L210]
        └─ [web] GET /api/ui/offices/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeAudit)  [L181–L206] status=200 [auth=Authentication.UserPolicy]
          └─ calls OfficeRepository.ReadQuery [L184]
          └─ query Office [L184]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L184]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /audit/find  (Dataverse.Api.Controllers.External.OfficesController.FindAudit)  [L46–L50] status=200
          └─ calls OfficeRepository.ReadQuery [L49]
          └─ query Office [L49]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L49]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/connections/reportance/wages  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetWages)  [L108–L114] status=200
        └─ [web] GET /api/ui/sync-monitor/failures  (Dataverse.Api.Controllers.UI.SyncMonitorController.Search)  [L36–L61] status=200 [auth=Authentication.AdminPolicy]
          └─ calls DataverseEntityFailureLogRepository.ReadQuery [L46]
          └─ query DataverseEntityFailureLog [L46]
            └─ reads_from DataverseEntityFailureLogs
        └─ [web] GET /api/ui/support-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.GetSupportUsersAsync)  [L36–L44] status=200 [auth=Authentication.AdminPolicy]
          └─ sends_request GetSupportUsersQuery -> GetSupportUsersQueryHandler [L42]
        └─ [web] GET /workflow/v1/deliverable-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditQuery)  [L116–L121] status=200
          └─ maps_to EntityAuditDto [L119]
          └─ calls DeliverableTypeRepository.ReadQuery [L119]
          └─ query DeliverableType [L119]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/accounting/connections/connection-summary  (Cirrus.Api.Controllers.Accounting.ConnectionsController.GetConnectionSummary)  [L31–L35] status=200 [auth=user]
        └─ [web] GET /api/connections/reportance/source/{datasetId}  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetSource)  [L60–L66] status=200
          └─ sends_request GetSourceQuery -> GetSourceQueryHandler [L63]
            └─ handled_by Workpapers.Next.CirrusServices.Queries.GetSourceQueryHandler.Handle [L20–L43]
              └─ uses_service CirrusHttp
                └─ method GetHttpResponseAsync [L39]
                  └─ ... (no implementation details available)
              └─ uses_service CirrusConfig
                └─ method GetBaseUrl [L38]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/connections/bgl360/funds/{fundId}/ledger/{startDate:datetime}/{endDate:datetime}  (Workpapers.Next.API.Controllers.Connections.Bgl360Controller.GetLedger)  [L49–L55] status=200
        └─ [web] GET /api/accounting/reports/page-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetAll)  [L55–L62] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to ReportPageLayoutLightDto [L58]
            └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
          └─ calls ReportPageLayoutRepository.ReadQuery [L58]
          └─ query ReportPageLayout [L58]
            └─ reads_from ReportPageLayouts
        └─ [web] GET /api/reportsettings/colour  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.GetPrimaryColour)  [L42–L49] status=200
          └─ uses_service UserService
            └─ method IsSuperUser [L45]
              └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser (see previous expansion)
          └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L46]
        └─ [web] GET /core/v1/users/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Get)  [L67–L72] status=200
          └─ maps_to UserDto [L70]
            └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
            └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
          └─ calls UserRepository.ReadQuery [L70]
          └─ query User [L70]
          └─ uses_service UserRepository
            └─ method ReadQuery [L70]
              └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetAll)  [L70–L74] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetLoanMatricesQuery -> GetLoanMatricesQueryHandler [L73]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatricesQueryHandler.Handle [L28–L46]
              └─ maps_to LoanMatrixLightDto [L41]
                └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixLightDto) [L996]
              └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
                └─ method ReadQuery [L41]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
        └─ [web] GET /api/ui/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetTaxAgentInfo)  [L222–L227] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to TaxAgentInfoDto [L225]
            └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L229]
          └─ uses_client ClientRepository [L225]
        └─ [web] GET /api/binder-column-template-sets/for-binder-type/{binderTypeId:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetForBinderType)  [L41–L45] status=200
          └─ sends_request GetBinderColumnTemplateSetForBinderType -> GetBinderColumnTemplateSetForBinderTypeHandler [L44]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetForBinderTypeHandler.Handle [L24–L45]
              └─ maps_to BinderColumnTemplateSetDto [L37]
                └─ automapper.registration WorkpapersMappingProfile (BinderColumnTemplateSet->BinderColumnTemplateSetDto) [L368]
              └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
                └─ method ReadQuery [L37]
                  └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
        └─ [web] GET /api/template-versions/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.DownloadTemplateVersion)  [L74–L82] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
          └─ uses_service StorageService
            └─ method CreateDownloadUrl [L81]
              └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl (see previous expansion)
          └─ uses_service UnitOfWork
            └─ method Table [L78]
              └─ implementation UnitOfWork.Table (see previous expansion)
          └─ uses_storage StorageService.CreateDownloadUrl [L81]
        └─ [web] GET /api/super/storage/repair-document/{documentId:Guid}/job/{jobId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.RepairDocument)  [L69–L170] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ uses_cache IDistributedCache.GetRecordAsync [read] [L149]
          └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
          └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L76]
          └─ calls DocumentVersionRepository.WriteQuery [L155]
          └─ write DocumentVersion [L155]
            └─ reads_from DocumentVersions
          └─ uses_service RequiredService
            └─ method AssignActiveTenant [L113]
              └─ ... (no implementation details available)
          └─ uses_service TenantService
            └─ method GetCurrentTenantAsync [L104]
              └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
          └─ sends_request UpdateDocumentVersionFileSizeCommand -> UpdateDocumentVersionFileSizeCommandHandler [L160]
            └─ handled_by Dataverse.Services.DocumentStorage.Commands.UpdateDocumentVersionFileSizeCommandHandler.Handle [L18–L40]
              └─ uses_service DocumentServiceFactory
                └─ method GetDocumentWithService [L30]
                  └─ implementation DocumentServiceFactory.GetDocumentWithService (see previous expansion)
          └─ sends_request CopyDocumentVersionCommand -> CopyDocumentVersionCommandHandler [L106]
            └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
              └─ calls DocumentVersionRepository (methods: Add,ReadQuery,WriteQuery) [L110]
              └─ uses_service DocumentServiceFactory
                └─ method GetDocumentWithService [L89]
                  └─ implementation DocumentServiceFactory.GetDocumentWithService (see previous expansion)
              └─ uses_service UserService
                └─ method GetUserId [L79]
                  └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ sends_request ValidateDocumentCommand -> ValidateDocumentCommandHandler [L85]
        └─ [web] GET /api/ui/workflow/jobs/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJob)  [L72–L81] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobDto [L77]
            └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L307]
            └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
            └─ converts_to JobExportDto [L312]
          └─ calls JobRepository.ReadQuery [L77]
          └─ query Job [L77]
            └─ reads_from Jobs
          └─ uses_service FirmSettingsService
            └─ method GetCurrentSettingsAsync [L79]
              └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync (see previous expansion)
          └─ uses_service UserService
            └─ method GetUserId [L79]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
          └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L75]
        └─ [web] GET /api/custom-statuses/by-type  (Workpapers.Next.API.Controllers.CustomStatusesController.GetAllFor)  [L49–L70] status=200
        └─ [web] GET /api/accounting/reports/notes/policies/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetPoliciesQuery -> GetPoliciesQueryHandler [L41]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPoliciesQueryHandler.Handle [L70–L227]
              └─ maps_to PolicyVariantForReportingSuiteLightDto [L199]
              └─ uses_service IControlledRepository<PolicyVariant> (Scoped (inferred))
                └─ method ReadQuery [L213]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyVariantRepository.ReadQuery
              └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
                └─ method ReadQuery [L153]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
              └─ uses_service IControlledRepository<Policy> (Scoped (inferred))
                └─ method ReadQuery [L149]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyRepository.ReadQuery
        └─ [web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/download  (DataGet.Api.Controllers.Integrations.IManageController.DownloadDocument)  [L248–L256] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetIManageDocumentDownloadContentQuery -> GetIManageDocumentDownloadContentQueryHandler [L255]
            └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentDownloadContentQueryHandler.Handle [L24–L41]
              └─ uses_service IManageService
                └─ method GetDocumentDownloadContent [L36]
                  └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocumentDownloadContent [L12-L223]
                    └─ uses_client IManageApiClient [L33]
                    └─ uses_service IManageApiClient
                      └─ method GetApiInformation [L33]
                        └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation (see previous expansion)
                    └─ uses_service RequestProcessor
                      └─ method GetAuthorisationUrl [L28]
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                        └─ +1 additional_requests elided
        └─ [web] GET /api/journals/{binderId:guid}/report/profit-reconciliation  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetProfitReconciliationReport)  [L174–L180] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetProfitReconciliationReportQuery -> GetProfitReconciliationReportQueryHandler [L179]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetProfitReconciliationReportQueryHandler.Handle [L31–L155]
              └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
                └─ method ReadQuery [L46]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L44]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L177]
        └─ [web] GET /api/ui/imanage/documents  (Dataverse.Api.Controllers.UI.IManageController.GetDocuments)  [L234–L250] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to IntegrationSettingsDto [L238]
            └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
          └─ calls IntegrationSettingsRepository.ReadQuery [L238]
          └─ query IntegrationSettings [L238]
            └─ reads_from IntegrationSettingss
          └─ uses_service IDatagetImanageService (AddTransient)
            └─ method GetDocumentsAsync [L244]
              └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentsAsync [L19-L225]
        └─ [web] GET /api/accounting/datasets/{id}/light  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetLight)  [L64–L71] status=200 [auth=user]
          └─ maps_to DatasetLightDto [L68]
            └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
          └─ calls DatasetRepository.ReadQuery [L68]
          └─ query Dataset [L68]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L67]
        └─ [web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/account-balance/{sourceAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetAccountBalance)  [L57–L62] status=200 [auth=user]
          └─ sends_request GetPrimaryProductionRowsDtoQuery -> GetPrimaryProductionRowsDtoQueryHandler [L60]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionRowsDtoQueryHandler.Handle [L47–L147]
              └─ maps_to PrimaryProductionConfigDto [L137]
                └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
              └─ maps_to SourceAccountForPrimaryProductionDto [L79]
                └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountForPrimaryProductionDto) [L276]
                └─ converts_to SourceAccountInJournalModel [L280]
              └─ uses_service IControlledRepository<PrimaryProductionConfig> (Scoped (inferred))
                └─ method ReadQuery [L137]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.PrimaryProductionConfigRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L131]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L126]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
              └─ uses_service GetTrialBalanceForDatesQuery
                └─ method Execute [L88]
                  └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
              └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
                └─ method ReadQuery [L79]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
        └─ [web] GET /api/super/offices/{officeId:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetUserAudit)  [L102–L127] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
          └─ calls OfficeRepository.ReadQuery [L105]
          └─ query Office [L105]
            └─ reads_from Offices
          └─ uses_service OfficeRepository
            └─ method ReadQuery [L105]
              └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery (see previous expansion)
        └─ [web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSource)  [L111–L133] status=200 [auth=user]
          └─ calls SourceRepository.ReadQuery [L119]
          └─ query Source [L119]
          └─ sends_request GetSourceAccountsWithCachedQuery -> GetSourceAccountsWithCachedQueryHandler [L127]
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L123]
        └─ [web] GET /api/workpapers/byoffice  (Workpapers.Next.API.Controllers.WorkpapersController.GetForOffice)  [L63–L67] status=200
        └─ [web] GET /workflow/v1/deliverable-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.Get)  [L52–L57] status=200
          └─ maps_to DeliverableTypeDto [L55]
            └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
            └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
          └─ calls DeliverableTypeRepository.ReadQuery [L55]
          └─ query DeliverableType [L55]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/accounting/ledger/accounts/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetLinkedSourceAccounts)  [L107–L121] status=200 [auth=user]
          └─ maps_to SourceAccountMappingDto [L115]
            └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
          └─ calls SourceAccountRepository.ReadQuery [L115]
          └─ query SourceAccount [L115]
            └─ reads_from SourceAccounts
          └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L114]
        └─ [web] GET /api/ui/documents/types  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetAll)  [L36–L48] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to DocumentTypeDto [L41]
            └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L420]
          └─ calls DocumentTypeRepository.ReadQuery [L41]
          └─ query DocumentType [L41]
            └─ reads_from DocumentTypes
        └─ [web] GET /workflow/v1/kanban-columns/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.Get)  [L48–L53] status=200
          └─ maps_to KanbanColumnDto [L51]
            └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
            └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
          └─ calls KanbanColumnRepository.ReadQuery [L51]
          └─ query KanbanColumn [L51]
            └─ reads_from KanbanColumns
        └─ [web] GET /workflow/v1/deliverable-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditsQuery)  [L102–L108] status=200
          └─ maps_to EntityAuditDto [L105]
          └─ calls DeliverableTypeRepository.ReadQuery [L105]
          └─ query DeliverableType [L105]
            └─ reads_from DeliverableTypes
        └─ [web] GET /api/workpapers/auto-reconcile  (Cirrus.Api.Controllers.Workpapers.AutoReconcileController.GetSourceInfo)  [L61–L75] status=200 [auth=Authentication.UserPolicy]
          └─ calls DatasetRepository.ReadQuery [L65]
          └─ query Dataset [L65]
          └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L63]
        └─ [web] GET /api/companies-house-gateway/company-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetCompanyDataAsync)  [L27–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetCompanyDataQuery -> GetCompanyDataQueryHandler [L30]
            └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetCompanyDataQueryHandler.Handle [L35–L64]
              └─ maps_to CompanyDataDto [L62]
              └─ uses_client CompaniesHouseInputGatewayClient [L58]
              └─ uses_service CompaniesHouseInputGatewayClient
                └─ method SendAsync [L58]
                  └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync (see previous expansion)
              └─ uses_service GovTalkEnvelopeCreator
                └─ method Create [L57]
                  └─ ... (no implementation details available)
        └─ [web] GET /api/ato/client-account-transactions/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetClientAccountTransactionSummary)  [L62–L72] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request GetClientAccountTransactionSummaryQuery -> GetClientAccountTransactionSummaryQueryHandler [L68]
        └─ [web] GET /api/workpaper-records  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.UpdateMatterCounts)  [L574–L585] status=200 [auth=AuthorizationPolicies.User]
        └─ [web] GET /api/ui/documents/documents/{id}/download  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DownloadDocument)  [L189–L196] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDocumentDownloadLink -> GetDocumentDownloadLinkHandler [L193]
          └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L192]
        └─ [web] GET /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetAll)  [L48–L63] status=200 [auth=Authentication.UserPolicy]
          └─ maps_to JobFilterSetLightDto [L53]
            └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetLightDto) [L343]
          └─ calls JobFilterSetRepository.ReadQuery [L53]
          └─ query JobFilterSet [L53]
            └─ reads_from JobFilterSets
          └─ uses_service UserService
            └─ method GetUserId [L51]
              └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId (see previous expansion)
        └─ [web] GET /workflow/v1/jobs/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Get)  [L52–L57] status=200
          └─ maps_to JobDto [L55]
            └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L307]
            └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
            └─ converts_to JobExportDto [L312]
          └─ calls JobRepository.ReadQuery [L55]
          └─ query Job [L55]
            └─ reads_from Jobs
        └─ [web] GET /api/accounting/reports/notes/disclosure-templates/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
          └─ sends_request GetDisclosureTemplatesQuery -> GetDisclosureTemplatesQueryHandler [L41]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplatesQueryHandler.Handle [L64–L215]
              └─ maps_to DisclosureVariantForReportingSuiteLightDto [L187]
              └─ uses_service IControlledRepository<DisclosureVariant> (Scoped (inferred))
                └─ method ReadQuery [L201]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureVariantRepository.ReadQuery
              └─ uses_service IControlledRepository<DisclosureTemplate> (Scoped (inferred))
                └─ method ReadQuery [L143]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureTemplateRepository.ReadQuery
        └─ [web] GET /workflow/v1/job-statuses/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.FullQuery)  [L88–L95] status=200
          └─ calls JobStatusRepository.ReadQuery [L92]
          └─ query JobStatus [L92]
            └─ reads_from DVS_JobStatuses
        └─ [web] GET /api/companies-house/document/{documentId}/content  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetDocumentContentLocation)  [L408–L416] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request GetDocumentContentLocationQuery -> GetDocumentContentLocationQueryHandler [L413]
            └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentContentLocationQueryHandler.Handle [L18–L31]
        └─ [web] GET /api/ui/templates/types  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.GetTypes)  [L53–L64] status=200 [auth=Authentication.UserPolicy]
          └─ uses_service ITemplateService (AddSingleton)
            └─ method GetTemplates [L56]
              └─ implementation Dataverse.Templates.TemplateService.GetTemplates (see previous expansion)
        └─ [web] GET /api/workpaper-records/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetHyperlinks)  [L399–L423] status=200 [auth=AuthorizationPolicies.User]
          └─ maps_to HyperlinkDto [L411]
          └─ calls WorkpaperRecordRepository.ReadQuery [L404]
          └─ query WorkpaperRecord [L404]
            └─ reads_from WorkpaperRecords
          └─ sends_request CanIAccessWorkpaperRecordQuery -> CanIAccessWorkpaperRecordQueryHandler [L402]
            └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.CanIAccessWorkpaperRecordQueryHandler.Handle [L35–L59]
              └─ uses_service RequestProcessor
                └─ method ProcessAsync [L57]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                  └─ +1 additional_requests elided
              └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
                └─ method ReadQuery [L55]
                  └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
              └─ uses_service RequestInfoService
                └─ method IsValidServiceAccountRequest [L53]
                  └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
        └─ [web] GET /api/ui/storage/process-zip/{contextId}  (Dataverse.Api.Controllers.UI.StorageController.ProcessZip)  [L103–L109] status=200 [auth=Authentication.UserPolicy]
        └─ [web] GET /api/accounting/matching-rules/file/{fileId}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.GetAll)  [L108–L116] status=200 [auth=user]
          └─ sends_request GetMatchingRulesForFileQuery -> GetMatchingRulesForFileHandler [L113]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetMatchingRulesForFileHandler.Handle [L35–L132]
              └─ maps_to AccountUltraLightDto [L108]
                └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
              └─ maps_to AccountTypeLightDto [L98]
                └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeLightDto) [L246]
              └─ maps_to MatchingRuleDto [L77]
                └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
              └─ uses_service IControlledRepository<Account> (Scoped (inferred))
                └─ method ReadQuery [L108]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
              └─ uses_service IControlledRepository<AccountType> (Scoped (inferred))
                └─ method ReadQuery [L98]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountTypeRepository.ReadQuery
              └─ uses_service IControlledRepository<MatchingRule> (Scoped (inferred))
                └─ method ReadQuery [L77]
                  └─ implementation Cirrus.Data.Repository.Accounting.Setup.MatchingRuleRepository.ReadQuery
              └─ uses_service IControlledRepository<File> (Scoped (inferred))
                └─ method ReadQuery [L70]
                  └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L68]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
        └─ [web] GET /api/accounting/divisions/{id}  (Cirrus.Api.Controllers.Accounting.Setup.DivisionsController.Get)  [L41–L47] status=200 [auth=user]
          └─ maps_to DivisionDto [L44]
            └─ automapper.registration CirrusMappingProfile (Division->DivisionDto) [L225]
          └─ calls DivisionRepository.ReadQuery [L44]
          └─ query Division [L44]
            └─ reads_from Divisions
        └─ [web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Get)  [L47–L51] status=200 [auth=user]
          └─ sends_request GetPrimaryProductionDtoQuery -> GetPrimaryProductionInfoQueryHandler [L50]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionInfoQueryHandler.Handle [L34–L79]
              └─ maps_to PrimaryProductionConfigDto [L62]
                └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
              └─ uses_service IControlledRepository<PrimaryProductionConfig> (Scoped (inferred))
                └─ method ReadQuery [L62]
                  └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.PrimaryProductionConfigRepository.ReadQuery
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L56]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
                └─ method ReadQuery [L51]
                  └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
        └─ [web] GET /api/accounting/reports/templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Get)  [L70–L74] status=200 [auth=user]
          └─ sends_request GetReportTemplateQuery -> GetReportTemplateQueryHandler [L73]
            └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportTemplateQueryHandler.Handle [L39–L150]
              └─ calls ReportContentRepository.LoadReadProperties [L126]
              └─ maps_to ReportTemplateColumnDto [L89]
                └─ converts_to ReportTemplateColumnCreateModel [L555]
              └─ maps_to ReportTemplateFinancialPageDetailDto [L84]
                └─ converts_to ReportTemplateFinancialPageModel [L559]
              └─ maps_to ReportTemplatePageDto [L67]
                └─ converts_to ReportTemplatePageCreateModel [L556]
                └─ converts_to ReportTemplatePageCreateUpdateModel [L557]
              └─ maps_to ReportTemplateDto [L60]
                └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateDto) [L534]
                └─ converts_to ReportTemplateCreateModel [L552]
              └─ maps_to ReportElementDto [L135]
              └─ maps_to ReportTemplateNotePageDetailDto [L128]
                └─ converts_to ReportTemplateNotePageModel [L558]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L64]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
              └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
                └─ method ReadQuery [L60]
                  └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
        └─ [web] GET /ledger/v1/datasets/  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.GetDatasetsMinimalQuery)  [L45–L52] status=200
          └─ calls DatasetRepository.ReadQuery [L48]
          └─ query Dataset [L48]
        └─ [web] GET /api/commands/{id:guid}  (Workpapers.Next.API.Controllers.Templates.CommandsController.Get)  [L66–L73] status=200
          └─ maps_to CommandDto [L69]
            └─ automapper.registration WorkpapersMappingProfile (Command->CommandDto) [L360]
          └─ calls CommandRepository.ReadQuery [L69]
          └─ query Command [L69]
            └─ reads_from Commands
        └─ [web] GET /api/binders/{binderId:Guid}/worksheets/  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.Search)  [L61–L91] status=200 [auth=AuthorizationPolicies.User]
          └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L72]
  └─ uses_client WorkpapersClient [L94]
    └─ calls CreateFirm (POST /api/dataverse/firms/) [L110]
      └─ remote_endpoint_expansion_suppressed (see previous expansion)
    └─ calls Search [L60]
      └─ remote_endpoint_expansion_suppressed (see previous expansion)
  └─ calls TenantRepository.WriteTable [L82]
  └─ write Tenant [L82]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L82]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand -> CreateOrUpdateSubscriptionCommandHandler [L113]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ remote_endpoints 18 (calls=28) scopes=service:28
      └─ GET (missing route) -> WorkpapersClient via WorkpapersClient [query] (x4)
      └─ POST /api/dataverse/firms -> WorkpapersClient via WorkpapersClient [command] (x4)
      └─ GET /api/accounting-file/{fileid}/accounts -> DataGet.Api via DataGetClient [query] (x3)
      └─ GET /api/central/databases -> CirrusClient via CirrusClient [query] (x2)
      └─ GET /api/central/storage-accounts -> CirrusClient via CirrusClient [query] (x2)
      └─ +13 more
      └─ missing_route_annotations 4
    └─ entities 109 (writes=8, reads=494)
      └─ User writes=0 reads=31
      └─ Binder writes=0 reads=25
      └─ Tenant writes=1 reads=23
      └─ Office writes=0 reads=21
      └─ Team writes=0 reads=16
      └─ +104 more
    └─ clients 12
      └─ CirrusClient
      └─ Client
      └─ ClientRepository
      └─ CompaniesHouseInputGatewayClient
      └─ DataGetClient
      └─ +7 more
    └─ services 62
      └─ AccountTypeRepository
      └─ ApiService
      └─ AtoService
      └─ CentralRepository
      └─ ConnectionService
      └─ +57 more
    └─ requests 331
      └─ AllProductsForUserQuery
      └─ AllStartersForProductQuery
      └─ AllWorkpaperRecordTemplatesForProductV2Query
      └─ BinderReportBaseQuery
      └─ CanIAccessBinderQuery
      └─ +326 more
    └─ handlers 299
      └─ AllProductsForUserQueryHandler
      └─ AllStartersForProductQueryHandler
      └─ AllWorkpaperRecordTemplatesForProductQueryHandler
      └─ CanIAccessBinderQueryHandler
      └─ CanIAccessClientQueryHandler
      └─ +294 more
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ storages 3
      └─ StorageService
      └─ StorageSettingsConfig
      └─ TableStorageService
    └─ mappings 318
      └─ AccountBalanceInfoDto
      └─ AccountDto
      └─ AccountInfoForWorkpapersDto
      └─ AccountLightDto
      └─ AccountLightListDto
      └─ +313 more

