CREATE OR ALTER PROCEDURE [dbo].[GKSSP_InsTeste]
	@Num_SeqlNegativ int, 
	@Num_ChavParc	 int
	AS

	/*
	Documentação
	Arquivo Fonte.....: Teste.sql
	Objetivo..........: Inserir um teste
	Autor.............: SMN - Douglas da Mata
 	Data..............: 28/05/2024
	Ex................: EXEC [dbo].[GKSSP_InsTeste]

	*/

	BEGIN
	
		INSERT INTO [dbo].[GKSPM_NegativacoesParcelas](Num_SeqlNegativ, Num_ChavParc)
			VALUES(@Num_SeqlNegativ, @Num_ChavParc)

	END
GO



CREATE OR ALTER  PROCEDURE [dbo].[GKSSP_SelDescricao]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Teste.sql
	Objetivo..........: Buscar testes
	Autor.............: SMN - Douglas da Mata
 	Data..............: 28/05/2024
	Ex................: EXEC [dbo].[GKSSP_SelDescricao]

	*/

	BEGIN

		SELECT	TOP 10	Cod_Nac AS Id,
						Nom_Cli AS Descricao
			FROM GKSLT_Clientes WITH(NOLOCK)

	END
				