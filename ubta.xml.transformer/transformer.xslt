<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns="http://www.usecasetests.com/SampleLib"
    xmlns:u="http://www.usecasetests.com/ubta.Schema">
  <?xml-stylesheet href="transformer.xsl" type="text/xsl"?>
  <xsl:output method="text"/>
  <xsl:template match ="/">
          <xsl:for-each select="RootNode">
            <xsl:value-of select="*" />
            <xsl:value-of select="." />
          </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>