[web] GET /api/ui/connections/has-connection  (Dataverse.Api.Controllers.UI.ConnectionsController.HasConnection)  [L42–L58] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetConnectionSummaryQuery -> GetConnectionSummaryQueryHandler [L47]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetConnectionSummaryQueryHandler.Handle [L22–L93]
      └─ maps_to ActiveConnectionDto [L55]
        └─ automapper.registration CirrusMappingProfile (SourceType->ActiveConnectionDto) [L208]
      └─ uses_client DataGetClient [L64]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L69]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
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
                └─ implementation Cirrus.DomainModel.Model.Firm.User.GetUserId [L18-L345]
            └─ uses_service Guid?
              └─ method GetUserId [L34]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetConnectionSummaryQuery
    └─ handlers 1
      └─ GetConnectionSummaryQueryHandler
    └─ mappings 1
      └─ ActiveConnectionDto

