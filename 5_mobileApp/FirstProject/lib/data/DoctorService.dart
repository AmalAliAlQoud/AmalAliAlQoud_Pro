import 'dart:convert';
import 'package:http/http.dart' as http;
import 'ConectionString.dart';


class DoctorService {
  Future<List<dynamic>> getDoctors() async {
    try {
      final response = await http.get(
        Uri.parse(ConectionString.doctors),
      );

      print('Response status: ${response.statusCode}');
      print('Response body: ${response.body}');

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);

        if (data is Map && data.containsKey('doctors')) {
          return data['doctors'] ?? [];
        }

        if (data is List) {
          return data;
        }
      }
    } catch (e) {
      print('Error fetching doctors: $e');
    }
    return [];
  }



  Future<List<dynamic>> getDoctorsBySpecialty(int specialtyId) async {
    try {
      final response = await http.get(
        Uri.parse('${ConectionString.doctorsBySpecialty}/$specialtyId'),
      );

      print('Doctors Response status: ${response.statusCode}');
      print('Doctors Response body: ${response.body}');

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);

        if (data is Map) {
          // الوصول للقائمة الموجودة داخل doctors -> result
          if (data['doctors'] is Map && data['doctors']['result'] is List) {
            return data['doctors']['result'];
          }
          // احتياطاً إذا كانت مباشرة داخل doctors
          if (data['doctors'] is List) {
            return data['doctors'];
          }
        }

        if (data is List) {
          return data;
        }
      }
    } catch (e) {
      print('Error fetching doctors: $e');
    }
    return [];
  }
}