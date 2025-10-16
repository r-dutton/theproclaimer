[web] PUT /core/v1/teams/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.Update)  [L142–L149] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L147]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

