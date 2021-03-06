﻿Documentação sobre Instalação e Desenvolvimento do Sistema

1.0 - Propósito
	
	O propósito deste documento é apresentar os principais aspectos relacionados à instalação e ao desenvolvimento do sistema, que possam servir como base para melhor utilização do aplicativo "Conversor Shipnet para Prosoft" e sua futuras manutenções.
	Ele se divide em duas partes básicas, a saber:
	Parte 1- Instalação; e
	Parte 2 – Desenvolvimento.

2.0 - Instalação
	
	A aplicação foi desenvolvida para que possa ser instalada diretamente na estação de trabalho (stand alone) do usuário. A instalação é realizada por meio do programa de instalação "Setup.exe" que, faz uso de uma forma padrão para a execução de instalação de softwares.

	2.1 – Requisitos mínimos 
	A estação de trabalho a ser utilizada pelo usuário deve atender aos seguintes requisitos mínimos:
	 - .Net Framework 4.2 ;
	Obs: Caso o Net Framework 4.2  não esteja instalado na máquina, o instalador solicitará que seja realizado o download e instalação.
 	- Windows 8.1 ou superior;
 	- 1 GB memória RAM;e
 -	 10 MBytes de espaço em disco.

3.0 - Desenvolvimento
	
	O aplicativo "Conversor Shipnet para Prosoft" contém arquivos de configuração de apoio, arquivos de log, e “outros arquivos”.

	3.1 – Arquivos de Configuração de apoio
	O aplicativo "Conversor Shipnet para Prosoft" contém 4 (quatro) arquivos de configuração com extensão "XML" para definir um DE-PARA das colunas do Excel para o layout de leitura e conversão dos arquivos. Os 4 (quatro) arquivos são:
	1-	SettingsCentroCusto.xml;
	2-	SettingsFornecedor.xml;
	3-	SettingsMovimentacao.xml;e
	4-	SettingsPais.xml.

		3.1.1 - SettingsCentroCusto.xml
		O SettingsCentroCusto.xml  é arquivo de configuração para leitura da planilha de Centro de Custos.
		O formato do código de Centro de Custo no Prosoft é dado pela concatenação do código do Navio e o código do Departamento
		Na 2ª linha estão definidas propriedades iniciais para leitura do arquivo:
		
		"InitialRow" - define o número da linha inicial de leitura da planilha;
		"LastColumn" - define o número da última coluna com dados na planilha;e
		"Sheet" - define o número da aba que contém as informações.
		
		A partir da 3ª linha são definidos os campos que devem ser informados e sua posição na planilha:
		"CCNavio" - Código do Navio;
		"Navio" - Nome do Navio;
		"CCDepartamento" - Código do Departamento;e
		"Departamento" - Nome do Departamento.

		3.1.2 - SettingsFornecedor.xml
		O SettingsFornecedor.xml é o arquivo de configuração para leitura da planilha de Fornecedores.
		Apenas algumas informações são importadas, com o objetivo de atender os requisitos de importação do Prosoft .
		O arquivo de fornecedores é utilizado também para busca de Fornecedores na importação de Movimentações, além da importação de Fornecedores.
		Na 2ª linha estão definidas propriedades iniciais para leitura do arquivo:
	
		"InitialRow" - define o número da linha inicial de leitura da planilha;
		"LastColumn" - define o número da última coluna com dados na planilha;e
		"Sheet" - define o número da aba que contém as informações.

		A partir da 3ª linha são definidos os campos que devem ser informados e sua posição na planilha:
		"Apelido" - Código do Fornecedor que será cadastrado;
		"CNPJCPFLivre" - Campo utilizado para a busca de informações do Fornecedores;
		"Nome";
		"Logradouro";
		"Complemento";
		"Municipio";
		"InscricaoEstadual";
		"InscricaoMunicipal";
		"CodigoPais";e
		"UF".

		3.1.3 - SettingsMovimentacao.xml
		O SettingsMovimentacao.xml é o arquivo de configuração para leitura da planilha de Movimentações.
		Apenas algumas informações são importadas, com o objetivo de atender os requisitos de importação do Prosoft. 
		O arquivo de movimentações é utilizado também para busca de Fornecedores com Movimentações na importação de Fornecedores, além da importação de Movimentações,	
		Na 2ª linha estão definidas propriedades iniciais para leitura do arquivo:

		"InitialRow" - define o número da linha inicial de leitura da planilha;
		"LastColumn" - define o número da última coluna com dados na planilha;e
		"Sheet" - define o número da aba que contém as informações.
		"ContaGeralDebito" - define qual a conta de DÉBITO padrão quando não for encontrada a conta
		"ContaCompensacao" - define qual a conta utilizada para compensar os registros de L2 quando houver mais de 200 lançamentos
		"ContaGeralCredito" - define qual a conta de CRÉDITO padrão quando não for encontrada a conta
		"ContaDeParaOrigem" - define quais contas devem ser substituídas pela "ContaDeParaDestino"
		"ContaDeParaDestino" - define a conta substituta das "ContaDeParaOrigem"
		"HistoricoCompensacao" - define o texto dos registros das contas de compensação para L2 quando houver mais de 200 lançamentos

		A partir da 3ª linha são definidos os campos que devem ser informados e sua posição na planilha:
		"NumeroDocumento" - Código da movimentação que será utilizado para identificação e aglomeração de lançamentos em LC1 e LC2;
		"CNPJCPFLivre" - Campo utilizado para a busca de informações do Fornecedores;
		"DataEscrituracao";
		"CodigoAcesso";
		"Terceiro" - Este campo é utilizado para buscar informações do Fornecedor;
		"Navio" - Este campo é utilizado para buscar informações do Centro de Custo;
		"Departamento" - Este campo é utilizado para buscar informações do Centro de Custo;
		"Valor";e
		"Historico".

		3.1.4-  SettingsPais.xml
		O SettingsPais.xml é o arquivo de configuração para leitura da planilha de País.
		O arquivo de país é utilizado na importação de Fornecedores
		Na 2ª linha estão definidas propriedades iniciais para leitura do arquivo:
	
		"InitialRow" - define o número da linha inicial de leitura da planilha;
		"LastColumn" - define o número da última coluna com dados na planilha;e
		"Sheet" - define o número da aba que contém as informações.

		A partir da 3ª linha são definidos os campos que devem ser informados e sua posição na planilha:
		"Sigla" - Este campo é utilizado para buscar informações do País;
		"Nome";e
		"Codigo" - Código do país no Prosoft e que será incluído no arquivo convertido de Fornecedores.

	3.2 - Arquivo de Log
	Todas as informações de conversão ou de erros do sistema são gravados no arquivo de log.
	O arquivo encontra-se na pasta do programa com o nome "Dev_App_Financeiro.log"

	3.3 – Outros arquivos
 	Na pasta do programa estão os arquivos de layout definidos pelo Prosoft.
 	O programa não utiliza estes arquivos, mas eles estão na pasta somente para apoio ao usuário, caso se visualiza a necessidade.

4.0 - Regras de Negócio
	
	No canto superior direito da  aba de "Fornecedores" e da aba de "Movimentações" existe um ícone de interrogação "?" que o ser pressionado informa as Regras de Negócio que foram aplicadas na conversão dos arquivos.
	Estas Regras de Negócio são uma concatenação de definições que foram acertadas ao longo do desenvolvimento da aplicação e que foram definidas em reuniões e troca de e-mails.

5.0- Instruções de Uso

	Em cada aba do programa existem instruções para a execução da conversão dos arquivos de Fornecedores e Movimentações.