module App.Constants

module URL =

    [<Literal>]
    let GitHub = @"https://github.com/Freymaurer/fa-poc"

    [<Literal>]
    let DEFAULT_DATAMAP =
        @"https://raw.githubusercontent.com/Freymaurer/test-github-get-access/refs/heads/main/assays/AnnotationContainerAssay/isa.datamap.xlsx"

    module MappingFile =
        [<Literal>]
        let Chlamy = @"https://raw.githubusercontent.com/CSBiology/ARCs/refs/heads/main/AnnotationARC/externals/AnnotationSnapshots/chlamy_jgi55.txt?token=GHSAT0AAAAAABYOVES6HB7NPG5JMMLMMPR62BCA4VA"